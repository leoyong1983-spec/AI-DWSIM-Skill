param([Parameter(Mandatory = $true)][string]$OutputDirectory)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
if (Test-Path -LiteralPath $OutputDirectory) { throw 'Use a new OutputDirectory.' }
New-Item -ItemType Directory -Path $OutputDirectory | Out-Null
$outputPath = (Resolve-Path -LiteralPath $OutputDirectory).ProviderPath
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$executable = Join-Path $outputPath 'runner_checks.exe'
& $compiler /nologo /target:exe /platform:x64 /main:RunnerChecks "/out:$executable" `
    /reference:Microsoft.CSharp.dll /reference:System.Web.Extensions.dll `
    (Join-Path $PSScriptRoot 'dwsim_runner.cs') (Join-Path $PSScriptRoot 'tests\runner_checks.cs')
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
& $executable
exit $LASTEXITCODE
