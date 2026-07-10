Write-Host ""
Write-Host "=== Prospector's Instinct Packager ===" -ForegroundColor Cyan
Write-Host ""

# Build
dotnet build

if ($LASTEXITCODE -ne 0)
{
    Write-Host ""
    Write-Host "Build failed. Packaging aborted." -ForegroundColor Red
    exit
}

$zipName = "ProspectorsInstinct_0.6.0-beta.zip"

# Delete previous package
if (Test-Path $zipName)
{
    Remove-Item $zipName -Force
}

# Create package
Compress-Archive `
    -Path ".\modinfo.json", ".\bin\Debug\net10.0\ProspectorsInstinct.dll" `
    -DestinationPath $zipName

# Copy to Vintage Story Mods folder
Copy-Item `
    $zipName `
    "$env:APPDATA\VintagestoryData\Mods\$zipName" `
    -Force

Write-Host ""
Write-Host "===================================" -ForegroundColor Green
Write-Host " Package Complete!" -ForegroundColor Green
Write-Host " ZIP: $zipName"
Write-Host " Copied to Mods folder."
Write-Host "===================================" -ForegroundColor Green