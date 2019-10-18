param (
  [string]$sourceFolder = $PSScriptRoot,
  [string]$artifactFolder = "c:\temp"
 )
 
Get-ChildItem $sourceFolder -Filter *.dacpac -Recurse | 
Foreach-Object { 
Write-Host "Docker CI File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}
