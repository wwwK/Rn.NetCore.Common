$workingDir            = $PSScriptRoot;
$sourceDir             = Join-Path $workingDir "src";
$publishDir            = Join-Path $workingDir "artifacts";
$toolsDir              = Join-Path $workingDir "tools";
$testPublishDir        = Join-Path $publishDir "t1-publih";
$testResultsDir        = Join-Path $publishDir "t1-results";
$testCoverageDir       = Join-Path $publishDir "t1-coverage";
$coverletExe           = "$toolsDir\coverlet.exe";
$reportGeneratorExe    = "$toolsDir\reportgenerator.exe"
$testCsprojPattern     = "*T1.Tests.csproj";
$buildConfiguration    = "Release";
$installCmd            = "";
$buildCmd              = "";
$currentPublishDir     = "";


# ==============================================================
# Cleanup
# ==============================================================
#
$cleanupDirectories = @();
$cleanupDirectories += $publishDir;
$cleanupDirectories += $toolsDir;
 
foreach ($cleanupDirectory in $cleanupDirectories) {
  if (Test-Path $cleanupDirectory) {
    Remove-item $cleanupDirectory -Recurse -Force | Out-Null;
  }
}

# ==============================================================
# Install tooling
# ==============================================================
#
if(!(Test-Path $coverletExe)){
  Write-Host "Installing: coverlet.console"
  $installCmd = "dotnet tool install coverlet.console --tool-path $toolsDir";
  Invoke-Expression -Command $installCmd -ErrorAction 'Continue';
}

if(!(Test-Path $reportGeneratorExe)){
  Write-Host "Installing: reportgenerator.exe"
  $installCmd = "dotnet tool install dotnet-reportgenerator-globaltool --tool-path `"$toolsDir`"";
  Invoke-Expression -Command $installCmd -ErrorAction 'Continue';
}

# ==============================================================
# Discover and Build test projects
# ==============================================================
#
$testProjects = Get-ChildItem -Path $sourceDir -Include $testCsprojPattern -Recurse -File

if ($testProjects.count -eq 0) {
  throw "No files matched the $testCsprojPattern pattern. The script cannot continue."
}

foreach ($testProject in $testProjects) {
  # Build test project
  $dllFileName = $testProject.BaseName + ".dll";
  $currentPublishDir = Join-Path $testPublishDir $testProject.BaseName
  $buildCmd = "dotnet build `"$testProject`" --configuration $buildConfiguration";
  #Write-Host "Running Build: $dllFileName";
  #Invoke-Expression $buildCmd;

  $searchPath    = Join-Path $testProject.Directory "bin\$buildConfiguration\"
  $dllFiles      = Get-ChildItem -Path $searchPath -Include $dllFileName -Recurse -File
  if($dllFiles.count -eq 0) {
    throw "Unable to find built DLL file";
  }

  $targetDllPath = $dllFiles | % { $_.FullName } | where { !($_ -match '\\ref\\') }
  if(!(Test-Path $targetDllPath)){
    throw "Unable to find test DLL file: $targetDllPath"
  }

  # Generate coverage report
  $testResultFileName   = Join-Path $testResultsDir "$( $testProject.BaseName )_results.trx";
  $coverageOutputDirTmp = Join-Path $testCoverageDir $testProject.BaseName;
  $coverageOutputDir    = Join-Path $coverageOutputDirTmp "\";

  # https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test
  $dotnetTestTargetArgs = @(
    "test",
    "$( $testProject.FullName )",
    "--logger:trx;LogFileName=$testResultFileName",
    "--configuration $buildConfiguration",
    "--no-build", 
    "--no-restore"
  );
  
  # https://www.jetbrains.com/help/dotcover/dotCover__Console_Runner_Commands.html
  $coverletDotnetArguments = @(
    $targetDllPath,
    "-t `"dotnet`"",
    "-a `"$dotnetTestTargetArgs`"",
    "-o $coverageOutputDir",
    "-f `"cobertura`"",
    "-f `"opencover`""
  );

  $coverletCmd = "$coverletExe $coverletDotnetArguments";
  Write-Output "Executing Coverlet with following arguments: $coverletCmd";
  Invoke-Expression -Command $coverletCmd;
}
