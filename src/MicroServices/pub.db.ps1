Set-Location $PSScriptRoot 
docker build --rm .\DB.Dockerfile -t ikemtz/nurse-cron-db/latest
docker tag ikemtz/nurse-cron-db/latest ikemtz/nurse-cron-db/latest
docker push ikemtz/nurse-cron-db/latest