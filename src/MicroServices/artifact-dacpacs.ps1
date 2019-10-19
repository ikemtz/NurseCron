param (
  [string]$sourceFolder = $PSScriptRoot,
  [string]$artifactFolder = "c:\temp"
 )
 
Get-ChildItem $sourceFolder -Filter *.dacpac -Recurse | 
Foreach-Object { 
Write-Host "DacPac File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}
 
Get-ChildItem $sourceFolder -Filter CI.Dockerfile -Recurse | 
Foreach-Object { 
Write-Host "Dockerfile File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}
