param (
  [string]$sourceFolder = $PSScriptRoot,
  [string]$domainName,
  [string]$artifactFolder
 )
 
Get-ChildItem $sourceFolder -Filter Docker*CI -Recurse | 
Foreach-Object { 
Write-Host "Docker CI File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}

Get-ChildItem $sourceFolder -Filter CI.$domainName.*Dockerfile -Recurse | 
Foreach-Object { 
Write-Host "Docker CI File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}

Get-ChildItem $sourceFolder -Filter *.$domainName.CiCd -Directory -Recurse | 
Foreach-Object { 
Write-Host "CI/CD Directory: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\$($_.Name)" -Recurse -Verbose -Force;
}

Get-ChildItem $sourceFolder -Filter *.CiCd -Directory -Recurse | 
Foreach-Object { 
Write-Host "Common CiCd Target Folder: $($_.FullName)";
Copy-Item "$sourceFolder\Common.CiCd\*" "$($_.FullName)\" -Verbose -Force;
}