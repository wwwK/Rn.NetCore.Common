param (
  $outputRoot = $PSScriptRoot
)

$outputRoot         = Join-Path $outputRoot "\";
$workingDir         = Join-Path $PSScriptRoot "\";
$sourceDir          = Join-Path $workingDir "src";
$artifactDir        = Join-Path $outputRoot "artifacts";
$publishDir         = Join-Path $artifactDir "publish";
$slnCommon          = Join-Path $sourceDir "Rn.NetCore.Common";
$buildConfiguration = "Release";
$buildCmd           = "";
$curPublishDir      = "";


# =============================================================================
# Build projects
# =============================================================================
#
$curPublishDir    = Join-Path $publishDir "Rn.NetCore.Common\";
$buildCmd         = "dotnet build $slnCommon --configuration $buildConfiguration --output `"$curPublishDir`""
Write-Host        "Running Build: $buildCmd"
Invoke-Expression $buildCmd;

