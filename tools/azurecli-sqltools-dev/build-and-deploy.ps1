Set-Location $PSScriptRoot;
$p = (Get-Location).Path;
$p = $p.Replace("\", "/");
docker build --rm -t ikemtz/azurecli-sqltools-dev .
docker push ikemtz/azurecli-sqltools-dev:latest
# docker run -v $p/devvol:/devvol --rm -it ikemtz/azurecli-sqltools-dev