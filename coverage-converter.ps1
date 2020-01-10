param (
  [string]$tempFolder = "C:\agent\_work\_temp",
  [string]$testResultsFolder = "c:\agent\_work\1\TestResults"
 )
 
 Write-Host "TempFolder: $($tempFolder)";
 Write-Host "testResultsFolder: $($sourceFolder)";

$pathToCC = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe";

Get-ChildItem $tempFolder -Filter *.coverage -Recurse | 
Foreach-Object { 
Write-Host "Coverage File: $($_.Name)";
$arguments = "analyze", "/output:$($_.FullName)xml", $_.FullName;
& $pathToCC $arguments
Copy-Item "$($_.FullName)xml" "$($testResultsFolder)\$($_.Name)xml" -Verbose;
}

Get-ChildItem $testResultsFolder -Filter *.coveragexml -Recurse -Verbose | 
Foreach-Object { 
Write-Host "Converted Coverage File: $($_.Name)"; 
}

Get-ChildItem $tempFolder -Filter *.coverag* -Recurse -Verbose | 
Foreach-Object { 
Remove-Item $_.FullName
}

exit 0;
