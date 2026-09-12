Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$pythonScript = Join-Path $scriptDir "validate_repo.py"

function Get-UsablePythonCommand {
    if (Get-Command py -ErrorAction SilentlyContinue) {
        $installed = (& py -0p 2>$null | Out-String).Trim()
        if ($installed -and $installed -notmatch "No installed Python") {
            if ($installed -match '3\.12') { return @("py", "-3.12") }
            Write-Warning 'Python 3.12 was not found; validating with the default Python 3 instead.'
            return @("py", "-3")
        }
    }

    $pythonCommand = Get-Command python -ErrorAction SilentlyContinue
    if ($pythonCommand -and $pythonCommand.Path -notlike "*WindowsApps*") {
        Write-Warning 'Python launcher unavailable; validating with python from PATH instead of py -3.12.'
        return @("python")
    }

    throw "Python 3 is required to run repository validation. Install Python from python.org or rely on the GitHub Actions 'Repo Hygiene' workflow."
}

$command = @(Get-UsablePythonCommand)
$arguments = @()
if ($command.Length -gt 1) {
    $arguments += $command[1..($command.Length - 1)]
}
$arguments += $pythonScript

& $command[0] @arguments
exit $LASTEXITCODE
