Remove-Item -path $PSScriptRoot/pub -Recurse
dotnet publish $PSScriptRoot -o pub
