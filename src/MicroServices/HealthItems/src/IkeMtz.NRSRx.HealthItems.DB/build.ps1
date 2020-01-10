Get-ChildItem -Path $PSScriptRoot -Filter *.sqlproj |  
Foreach-Object { 
& 'C:\Program Files (x86)\Microsoft Visual Studio\2017\*\MSBuild\15.0\Bin\MSBuild.exe' $_.FullName 
}