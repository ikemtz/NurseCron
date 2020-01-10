# 
# start.ps1 
# 
Remove-Item -path ./pub -Recurse 
dotnet publish -o pub 
docker build --rm . -t ikemtz.nrsrx.employees.api:latest 
docker tag ikemtz.nrsrx.employees.api:latest ikemtz/nrsrx:employees.api_latest 
docker push ikemtz/nrsrx:employees.api_latest 
