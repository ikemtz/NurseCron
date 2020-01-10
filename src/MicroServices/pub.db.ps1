Set-Location $PSScriptRoot 
docker build --rm .\DB.Dockerfile -t ikemtz/nrsrx/db:latest 
docker tag ikemtz/nrsrx/db:latest ikemtz/nrsrx:db_latest 
docker push ikemtz/nrsrx:db_latest