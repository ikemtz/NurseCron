param (
  [string]$sourceFolder = $PSScriptRoot,
  [string]$domainName,
  [string]$artifactFolder
 )

Write-Host "***** Distributing Common CiCd Folder *****";
Get-ChildItem $sourceFolder -Filter *$domainName.CiCd -Directory -Recurse | 
Foreach-Object { 
Write-Host "Common CiCd Target Folder: $($_.FullName)";
Copy-Item "$sourceFolder\Common_CiCd\*" "$($_.FullName)\" -Verbose -Force;
}

Write-Host "***** Copying Docker files to artifact folder *****";
Get-ChildItem $sourceFolder -Filter Docker*CI -Recurse | 
Foreach-Object { 
Write-Host "Docker CI File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}

Write-Host "***** Copying CI $Domain Docker files to artifact folder *****";
Get-ChildItem $sourceFolder -Filter CI.$domainName.*Dockerfile -Recurse | 
Foreach-Object { 
Write-Host "Docker CI File: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\" -Verbose -Force;
}

Write-Host "***** Copying  $Domain CiCd folder items to artifact folder *****";
Get-ChildItem $sourceFolder -Filter *.$domainName.CiCd -Directory -Recurse | 
Foreach-Object { 
Write-Host "CI/CD Directory: $($_.Name)";
Copy-Item "$($_.FullName)" "$($artifactFolder)\$($_.Name)" -Recurse -Verbose -Force;
}
