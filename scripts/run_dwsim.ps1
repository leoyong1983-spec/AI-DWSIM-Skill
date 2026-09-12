[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string]$DwsimDirectory,
    [Parameter(Mandatory = $true)][string]$OutputDirectory,
    [string]$ModelPath,
    [ValidateRange(1, 3600)][int]$TimeoutSeconds = 120
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$outputPath = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($OutputDirectory)
if (Test-Path -LiteralPath $outputPath) {
    throw 'OutputDirectory must be a NEW directory. Existing results are never overwritten.'
}
$installPath = (Resolve-Path -LiteralPath $DwsimDirectory).ProviderPath
if (-not (Test-Path -LiteralPath (Join-Path $installPath 'DWSIM.Automation.dll') -PathType Leaf)) {
    throw 'DWSIM.Automation.dll was not found in DwsimDirectory.'
}
$inputPath = ''
if ($ModelPath) {
    $inputPath = (Resolve-Path -LiteralPath $ModelPath).ProviderPath
    if (-not (Test-Path -LiteralPath $inputPath -PathType Leaf) -or
        [IO.Path]::GetExtension($inputPath) -notin @('.dwxmz', '.dwxml')) {
        throw 'ModelPath must be an existing .dwxmz or .dwxml file.'
    }
}
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
if (-not (Test-Path -LiteralPath $compiler)) {
    throw 'Windows x64 and the .NET Framework 4.x C# compiler are required.'
}

New-Item -ItemType Directory -Path $outputPath -ErrorAction Stop | Out-Null
$executable = Join-Path $outputPath 'dwsim_runner.exe'
$statusPath = Join-Path $outputPath 'run-status.json'
$runner = $null
$stage = 'compile'
try {
    $compileOutput = & $compiler /nologo /target:exe /platform:x64 "/out:$executable" `
        /reference:Microsoft.CSharp.dll /reference:System.Web.Extensions.dll `
        (Join-Path $PSScriptRoot 'dwsim_runner.cs') 2>&1
    $compileExit = $LASTEXITCODE
    $compileOutput | Out-File -LiteralPath (Join-Path $outputPath 'compile.log') -Encoding utf8
    if ($compileExit -ne 0) { throw "Runner compilation failed. See compile.log (exit $compileExit)." }
    $assemblyConfig = Join-Path $installPath 'DWSIM.Automation.dll.config'
    if (Test-Path -LiteralPath $assemblyConfig) {
        Copy-Item -LiteralPath $assemblyConfig -Destination "$executable.config"
    }

    $stage = 'launch'
    # Quoted argv goes directly to the executable, never through another shell.
    $arguments = @($installPath, $outputPath, $inputPath) | ForEach-Object {
        '"' + [regex]::Replace($_, '(\\+)$', '$1$1') + '"'
    }
    $runner = Start-Process -FilePath $executable -ArgumentList $arguments -PassThru `
        -WindowStyle Hidden -WorkingDirectory $installPath `
        -RedirectStandardOutput (Join-Path $outputPath 'stdout.log') `
        -RedirectStandardError (Join-Path $outputPath 'stderr.log')
    # Keep a handle so Windows PowerShell 5.1 can retrieve ExitCode after WaitForExit.
    $null = $runner.Handle
    $stage = 'timeout'
    if (-not $runner.WaitForExit($TimeoutSeconds * 1000)) {
        $runner.Kill()
        $runner.WaitForExit()
        throw "DWSIM worker exceeded $TimeoutSeconds seconds and was stopped (PID $($runner.Id))."
    }
    $runner.WaitForExit()
    $stage = 'worker'
    if (-not (Test-Path -LiteralPath $statusPath)) { throw 'Worker exited without run-status.json.' }
    $status = Get-Content -LiteralPath $statusPath -Raw -Encoding UTF8 | ConvertFrom-Json
    if ($runner.ExitCode -ne 0 -and $status.status -eq 'passed') {
        throw "Worker reported success but exited with code $($runner.ExitCode)."
    }
    if ($runner.ExitCode -ne 0 -or $status.status -ne 'passed') {
        Write-Error -Message "DWSIM run failed at $($status.stage). See $statusPath" -ErrorAction Continue
        exit 1
    }
    Write-Output "DWSIM calculation and save/reload verification passed. Evidence: $statusPath"
}
catch {
    if (Test-Path -LiteralPath $statusPath) {
        Copy-Item -LiteralPath $statusPath -Destination (Join-Path $outputPath 'worker-status.json')
    }
    $failure = [ordered]@{
        schema_version = 1
        status = 'failed'
        stage = $stage
        error = $_.Exception.Message
        completed_utc = [DateTime]::UtcNow.ToString('o')
    }
    $failure | ConvertTo-Json | Set-Content -LiteralPath $statusPath -Encoding UTF8
    Write-Error -Message $failure.error -ErrorAction Continue
    exit 1
}
finally {
    if ($runner) { $runner.Dispose() }
}
exit 0
