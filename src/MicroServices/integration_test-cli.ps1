#az login 
#az account set --subscription e92419ad-e3e7-488a-81d4-794b498de73e 
 
Set-Location $PSScriptRoot 
Set-Location .\Certifications\src\NurseCron.Certifications.Tests 
az storage blob download --account-name nrsrx --container pipeline --name cert-appsettings.json --file appsettings.json 
 
Set-Location $PSScriptRoot 
Set-Location .\Competencies\src\NurseCron.Competencies.Tests 
az storage blob download --account-name nrsrx --container pipeline --name comp-appsettings.json --file appsettings.json 
 
Set-Location $PSScriptRoot 
Set-Location .\Employees\src\NurseCron.Employees.Tests 
az storage blob download --account-name nrsrx --container pipeline --name empl-appsettings.json --file appsettings.json 
 
Set-Location $PSScriptRoot 
Set-Location .\HealthItems\src\NurseCron.HealthItems.Tests 
az storage blob download --account-name nrsrx --container pipeline --name hlti-appsettings.json --file appsettings.json 
