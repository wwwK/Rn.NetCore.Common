$PublishDirectoryName  = "artifacts";
$Configuration         = "Release";

# Setup directories
$workingDir       = $PSScriptRoot;
$sourceDir        = Join-Path $workingDir "src";
$slnCommon        = Join-Path $sourceDir "Rn.NetCore.Common";
$slnWebCommon     = Join-Path $sourceDir "Rn.NetCore.WebCommon";
$slnMetricsRabbit = Join-Path $sourceDir "Rn.NetCore.Metrics.Rabbit";
$buildCmd         = ""

# Build projects
$buildCmd = "dotnet build $slnCommon --configuration $Configuration"
Write-Host "Running Build: $buildCmd"
Invoke-Expression $buildCmd;

$buildCmd = "dotnet build $slnWebCommon --configuration $Configuration" 
Write-Host "Running Build: $buildCmd"
Invoke-Expression $buildCmd;

$buildCmd = "dotnet build $slnMetricsRabbit --configuration $Configuration" 
Write-Host "Running Build: $buildCmd"
Invoke-Expression $buildCmd;
