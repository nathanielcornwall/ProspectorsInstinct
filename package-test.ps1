param(
    [string]$TestVersion = "0.9.4-dev.4"
)

$ErrorActionPreference = "Stop"
$projectRoot = $PSScriptRoot
$releaseRoot = Join-Path $projectRoot "release"
$stagingDir = Join-Path $releaseRoot "test-staging"
$zipPath = Join-Path $releaseRoot "ProspectorsInstinct-latest-test.zip"
$pendingZip = Join-Path $releaseRoot "ProspectorsInstinct-pending-test.zip"

# Restrict recursive cleanup to this script's one disposable staging folder.
function Remove-TestStaging {
    $resolved = [System.IO.Path]::GetFullPath($stagingDir)
    $expected = [System.IO.Path]::GetFullPath((Join-Path $projectRoot "release\test-staging"))
    if ($resolved -ne $expected) { throw "Unexpected staging path: $resolved" }
    if (Test-Path -LiteralPath $resolved) {
        Remove-Item -LiteralPath $resolved -Recurse -Force
    }
}

dotnet build (Join-Path $projectRoot "ProspectorsInstinct.csproj") -c Release
if ($LASTEXITCODE -ne 0) { throw "Build failed; previous test package retained." }

Remove-TestStaging
try {
    New-Item -ItemType Directory -Path $stagingDir -Force | Out-Null
    Copy-Item -LiteralPath (Join-Path $projectRoot "bin\Release\net10.0\ProspectorsInstinct.dll") -Destination $stagingDir
    Copy-Item -LiteralPath (Join-Path $projectRoot "assets") -Destination (Join-Path $stagingDir "assets") -Recurse
    $testInfo = Get-Content -LiteralPath (Join-Path $projectRoot "modinfo.json") -Raw | ConvertFrom-Json
    $testInfo.version = $TestVersion
    $testInfo | ConvertTo-Json -Depth 10 | Set-Content -LiteralPath (Join-Path $stagingDir "modinfo.json") -Encoding utf8
    Compress-Archive -Path (Join-Path $stagingDir "*") -DestinationPath $pendingZip -Force

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($pendingZip)
    try {
        foreach ($required in @("modinfo.json", "ProspectorsInstinct.dll", "assets/prospectorsinstinct/lang/en.json")) {
            if ($archive.Entries.FullName -notcontains $required) { throw "Package missing $required" }
        }
    } finally { $archive.Dispose() }
    Move-Item -LiteralPath $pendingZip -Destination $zipPath -Force
    Write-Output "Verified test package: $zipPath"
} finally {
    Remove-TestStaging
    if (Test-Path -LiteralPath $pendingZip) { Remove-Item -LiteralPath $pendingZip -Force }
}
