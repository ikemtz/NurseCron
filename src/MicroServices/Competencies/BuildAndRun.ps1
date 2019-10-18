docker-compose down
docker-compose rm
Get-ChildItem -Path $PSScriptRoot -Filter build.ps1 -Recurse | 
Foreach-Object {
& $_.Fullname
}
docker-compose build
docker-compose up