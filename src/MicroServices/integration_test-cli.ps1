#az login 
#az account set --subscription e92419ad-e3e7-488a-81d4-794b498de73e 
 
Set-Location $PSScriptRoot 
Set-Location .\Certifications\src\IkeMtz.NRSRx.Certifications.Tests 
az storage blob download --account-name nrsrx --container pipeline --name cert-appsettings.json --file appsettings.json 
 
Set-Location $PSScriptRoot 
Set-Location .\Competencies\src\IkeMtz.NRSRx.Competencies.Tests 
az storage blob download --account-name nrsrx --container pipeline --name comp-appsettings.json --file appsettings.json 
 
Set-Location $PSScriptRoot 
Set-Location .\Employees\src\IkeMtz.NRSRx.Employees.Tests 
az storage blob download --account-name nrsrx --container pipeline --name empl-appsettings.json --file appsettings.json 
 
Set-Location $PSScriptRoot 
Set-Location .\HealthItems\src\IkeMtz.NRSRx.HealthItems.Tests 
az storage blob download --account-name nrsrx --container pipeline --name hlti-appsettings.json --file appsettings.json 
