param (
  [string]$sourceFolder = $PSScriptRoot,
  [string]$artifactFolder =  "c:\temp"
 )
 
Get-ChildItem $sourceFolder -Filter Docker*CI -Recurse | 
Foreach-Object { 
Write-Host "Docker CI File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}

Get-ChildItem $sourceFolder -Filter CI.*Dockerfile -Recurse | 
Foreach-Object { 
Write-Host "Docker CI File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}

Get-ChildItem $sourceFolder -Filter *.CiCd -Directory -Recurse | 
Foreach-Object { 
Write-Host "CI/CD Directory: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\$($_.Name)" -Recurse -Verbose -Force;
}
