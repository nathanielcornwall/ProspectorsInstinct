Write-Host ""
Write-Host "=== Prospector's Instinct Packaging ===" -ForegroundColor Cyan
Write-Host ""

$version = "0.9.3"
$zipName = "ProspectorsInstinct-v$version.zip"

$releaseRoot = ".\release"
$stagingDir = "$releaseRoot\v$version"
$zipPath = "$releaseRoot\$zipName"

$dllPath = ".\bin\Release\net10.0\ProspectorsInstinct.dll"
$modInfoPath = ".\modinfo.json"

# -----------------------------
# Build
# -----------------------------

Write-Host "Building Release configuration..." -ForegroundColor Yellow

dotnet build -c Release

if ($LASTEXITCODE -ne 0)
{
    Write-Host ""
    Write-Host "Build failed. Packaging aborted." -ForegroundColor Red
    exit 1
}

# -----------------------------
# Verify required files
# -----------------------------

if (-not (Test-Path $dllPath))
{
    Write-Host "Release DLL not found: $dllPath" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $modInfoPath))
{
    Write-Host "modinfo.json not found." -ForegroundColor Red
    exit 1
}

# -----------------------------
# Prepare staging directory
# -----------------------------

Remove-Item $stagingDir -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item $zipPath -Force -ErrorAction SilentlyContinue

New-Item `
    -ItemType Directory `
    -Path $stagingDir `
    -Force | Out-Null

Copy-Item `
    $modInfoPath `
    "$stagingDir\modinfo.json"

Copy-Item `
    $dllPath `
    "$stagingDir\ProspectorsInstinct.dll"

# -----------------------------
# Create package
# -----------------------------

Compress-Archive `
    -Path `
        "$stagingDir\modinfo.json",
        "$stagingDir\ProspectorsInstinct.dll" `
    -DestinationPath $zipPath

if (-not (Test-Path $zipPath))
{
    Write-Host "Package creation failed." -ForegroundColor Red
    exit 1
}

# -----------------------------
# Verify package contents
# -----------------------------

Add-Type -AssemblyName System.IO.Compression.FileSystem

$zip = [System.IO.Compression.ZipFile]::OpenRead(
    (Resolve-Path $zipPath)
)

Write-Host ""
Write-Host "Package contents:" -ForegroundColor Cyan

$zip.Entries |
    Select-Object FullName, Length |
    Format-Table -AutoSize

$zip.Dispose()

# -----------------------------
# Copy to Vintage Story
# -----------------------------

$modsFolder =
    "$env:APPDATA\VintagestoryData\Mods"

if (Test-Path $modsFolder)
{
    Copy-Item `
        $zipPath `
        "$modsFolder\$zipName" `
        -Force

    Write-Host "Copied package to Vintage Story Mods folder." -ForegroundColor Green
}
else
{
    Write-Host "Vintage Story Mods folder was not found." -ForegroundColor Yellow
}

# -----------------------------
# Complete
# -----------------------------

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "Package Complete!" -ForegroundColor Green
Write-Host "Version : $version"
Write-Host "ZIP     : $zipPath"
Write-Host "========================================" -ForegroundColor Green