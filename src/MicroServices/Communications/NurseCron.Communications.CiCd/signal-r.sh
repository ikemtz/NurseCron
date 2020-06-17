#!/bin/bash
#
if [ -z $2 ]; then
echo Bash shell use
echo $0 [D,Q,U,P] {subscriptionName OR subscriptionId}
exit 1
fi

if [ $2 ]; then 
az account set -s $2
fi

# parameter variables
export envUpper=$(echo $1 | tr a-z A-Z)
export envLower=$(echo $1 | tr A-Z a-z)

# Common Setup Variables
export location="eastus"
export planRgName=$envUpper"-NurseCron"
export appsRgName=$envUpper"-NurseCron"
export planName=$envLower"-ap-core-nrcrn"
export app1Name=$envLower"-wa-coms-nrcrn"
export dockerUrl=$(echo "https://index.docker.io")

export identityProvider="https://nrsrx-demo.auth0.com/"
# Service specific  
export entityName="Communications"
export validAudiences="$envUpper-NurseCron,COMS-DEV"
export ainName=$envLower"-ai-core-nrcrn"
export dockerImageName="ikemtz/nurse-cron-communications:signalr_latest"
export sqlDbName=$envLower"-db-core-nrcrn"

echo Spinning up resources for $planRgName

echo Create Web App Plan Resource Group $planRgName
export planRgId=$(az group create --location $location --name $planRgName | jq -r '. | .id')
echo Plan Group Id: ${planRgId}
echo

echo Create Web App Plan $planName
export appPlanId=$(az appservice plan create --resource-group $planRgName --name $planName --is-linux --sku B2 | jq -r '. | .id')
echo App Plan Id: ${appPlanId}
echo

echo Create Web Apps Resource Group $appsRgName
export appsRgId=$(az group create --location $location --name $appsRgName | jq -r '. | .id')
echo Apps Group Id: ${appPlanId}
echo

echo Create App Insights $ainName
export appInsightsKey=$(az resource create \
    --resource-group $appsRgName \
    --resource-type "Microsoft.Insights/components" \
    --location $location \
    --name $ainName \
    --properties '{"Application_Type":"web","Flow_Type":"Redfield","Request_Source":"IbizaAIExtension"}' \
    | jq -r '. | .properties.InstrumentationKey')
echo App Insights Key: ${appInsightsKey}
echo

# this is necessary, otherwise create web app call will fail
rgCheck=$(az webapp list --query "[?name=='$app1Name'].{group:resourceGroup}" | jq -r '.[0].group')
if [ $rgCheck != $appsRgName ]; then
    echo Creating Web App $app1Name
    app1id=$(az webapp create \
        --name $app1Name \
        --plan $planName \
        --resource-group $planRgName \
        --deployment-container-image-name $dockerImageName \
        | jq -r '. | .id')
    echo App Id 1: ${app1id}
    echo

    if [ $appsRgName != $planRgName ]; then    
        echo Moving Web App to $appsRgName
        az resource move --destination-group $appsRgName --ids $app1id
    fi
fi

echo Web App $app1Name already exists
app1id=$(az webapp show --name $app1Name --resource-group $appsRgName | jq -r '.id')

echo Configuring Web App $app1Name
app1Config=$(az webapp config container set --name $app1Name --resource-group $appsRgName \
    --docker-custom-image-name $dockerImageName \
    --docker-registry-server-url $dockerUrl \
    | jq -r '.')

echo App Id 1: ${app1id}
echo

echo Configuring Web App InstrumentationKey $appInsightsKey
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings InstrumentationKey="$appInsightsKey" | jq -r '.')
echo Configuring Web App IdentityAudiences $validAudiences
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings IdentityAudiences="$validAudiences" | jq -r '.')
echo Configuring Web App IdentityProvider $identityProvider
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings IdentityProvider="$identityProvider" | jq -r '.')

echo Configuring Web App - Disabling Client affinity and making site HTTPS only
temp=$(az webapp update --resource-group $appsRgName --name $app1Name --client-affinity-enabled false --https-only true)

echo Configuring Web App - enabling docker CI
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings DOCKER_ENABLE_CI=true | jq -r '.')

echo Configuring Web App - disabling FTP access - enabling alwayson
temp=$(az webapp config set --resource-group $appsRgName --name $app1Name --ftps-state Disabled --always-on true)

echo Enabling Web Sockets
temp=$(az webapp config set --resource-group $appsRgName --name $app1Name --web-sockets-enabled true)

bash ./restart-web-app.sh
