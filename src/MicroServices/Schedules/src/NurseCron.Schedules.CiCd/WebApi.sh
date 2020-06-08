#!/bin/bash
#
if [ -z $2 ]; then
echo Bash shell use
echo $0 [D,Q,U,P] {sqlAdminPassword} {subscriptionName OR subscriptionId}
exit 1
fi

if [ $3 ]; then 
az account set -s $3
fi

echo 'Spinning up resources for Invoices and receivables'
# parameter variables
export envUpper=$(echo $1 | tr a-z A-Z)
export envLower=$(echo $1 | tr A-Z a-z)
export sqlAdminPass=$2

# Common Setup Variables
export location="eastus"
export planRgName=$envUpper"-NurseCron"
export appsRgName=$envUpper"-NurseCron"
export planName=$envLower"-ap-core-nrcrn"
export app1Name=$envLower"-wa-scda-nrcrn"
export dockerUrl="https://index.docker.io"

export sqlRgName=$envUpper"-NurseCron"
export sqlSrvName=$envLower"-ss-core-nrcrn"
export sqlAdminUser=$envUpper"_NurseCronAdminUser"
export newSqlUserName=$(echo $app1Name | tr - x)
export identityProvider="https://nrsrx-demo.auth0.com/"

export serviceBusRgName=$envUpper"-NurseCron"
export serviceBusNamespace=$envLower"-sb-core-nrcrn"

# Service specific 
export entityTypes=( "Schedules" )
export eventTypes=( "Created" "Updated" "Deleted" )
export swaggerClientId="pIvS9gx3454OZZPkJ5xEUPtht0vcq4vw"
export swaggerAudience=$envUpper"-ScdA"
export validAudiences="$envUpper-NurseCron,$swaggerAudience"
export ainName=$envLower"-ai-core-nrcrn"
export dockerImageName="ikemtz/nurse-cron-schedules:webapi_latest"
export sqlDbName=$envLower"-db-core-nrcrn"

# New Randomized SQL Password
export newSqlPass=$(openssl rand -base64 48 | awk '{gsub(/[\/|+|=|;]/, "im")};1')

echo Spinning up resources for $sqlRgName

echo Create Sql Resource Group $sqlRgName
export sqlRgId=$(az group create --location $location --name $sqlRgName | jq -r '. | .id')
echo Plan Group Id: ${sqlRgId}
echo

echo Creating SQL server $sqlSrvName
export sqlSrvId=$(az sql server create --location $location --resource-group $sqlRgName --name $sqlSrvName --admin-user $sqlAdminUser --admin-password $sqlAdminPass | jq -r '. | .id')
echo Sql Server Id: ${sqlSrvId}

echo Create Database $sqlDbName
export sqlDbId=$(az sql db create \
	--resource-group $sqlRgName \
	--server $sqlSrvName \
	--name $sqlDbName \
	--service-objective Basic \
     | jq -r '. | .id')
echo Sql Database Id: ${sqlDbId}
echo

echo Create Web App Plan Resource Group $planRgName
export planRgId=$(az group create --location $location --name $planRgName | jq -r '. | .id')
echo Plan Group Id: ${planRgId}
echo

echo Create Web App Plan $planName
export appPlanId=$(az appservice plan create --resource-group $planRgName --name $planName --is-linux --sku B1 | jq -r '. | .id')
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

echo App Id 1: $app1id
echo

echo Configuring Web App InstrumentationKey $appInsightsKey
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings InstrumentationKey="$appInsightsKey" | jq -r '.')
echo Configuring Web App IdentityAudiences $validAudiences
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings IdentityAudiences="$validAudiences" | jq -r '.')
echo Configuring Web App IdentityProvider $identityProvider
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings IdentityProvider="$identityProvider" | jq -r '.')
echo Configuring Web App SwaggerIdentityProviderUrl $identityProvider
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings SwaggerIdentityProviderUrl="$identityProvider" | jq -r '.')
echo Configuring Web App SwaggerClientId $swaggerClientId
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings SwaggerClientId="$swaggerClientId" | jq -r '.')

swagName="$swaggerAudience (Swagger Client)"
echo Configuring Web App SwaggerAppName $swagName
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings SwaggerAppName="$swagName" | jq -r '.')

echo Configuring Web App - Disabling Client affinity and making site HTTPS only
temp=$(az webapp update --resource-group $appsRgName --name $app1Name --client-affinity-enabled false --https-only true)

echo Configuring Web App - enabling docker CI
temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings DOCKER_ENABLE_CI=true | jq -r '.')

echo Configuring Web App - disabling FTP access - enabling alwayson
temp=$(az webapp config set --resource-group $appsRgName --name $app1Name --ftps-state Disabled --always-on true)

echo Checking existance of DbConnectionString AppSetting
connectionString=$(az webapp config appsettings list --resource-group $appsRgName --name $app1Name \
    | jq '.[] | select(.name=="DbConnectionString") | .value' \
    | tr ' ' 'x')
    
# we need this check to ensure we are not overriding an existing password
if [ -z $connectionString ]; then
    echo 'Generating SQL Scripts'
    createLoginQuery="CREATE LOGIN $newSqlUserName WITH PASSWORD='$newSqlPass'"
    alterLoginQuery="ALTER LOGIN $newSqlUserName WITH PASSWORD='$newSqlPass'"
    createUserQuery="CREATE USER $newSqlUserName FOR LOGIN $newSqlUserName WITH DEFAULT_SCHEMA = dbo"
    addReaderRoleQuery="EXEC sp_addrolemember 'db_datareader', '$newSqlUserName'"
    addWriterRoleQuery="EXEC sp_addrolemember 'db_datawriter', '$newSqlUserName'"

    echo 'Creating SQL User'
    sqlcmd -S $sqlSrvName.database.windows.net -U $sqlAdminUser -P $sqlAdminPass -Q "$createLoginQuery"
    sqlcmd -S $sqlSrvName.database.windows.net -U $sqlAdminUser -P $sqlAdminPass -Q "$alterLoginQuery"
    sqlcmd -S $sqlSrvName.database.windows.net -U $sqlAdminUser -P $sqlAdminPass -d "$sqlDbName" -Q "$createUserQuery"
    sqlcmd -S $sqlSrvName.database.windows.net -U $sqlAdminUser -P $sqlAdminPass -d "$sqlDbName" -Q "$addReaderRoleQuery"
    sqlcmd -S $sqlSrvName.database.windows.net -U $sqlAdminUser -P $sqlAdminPass -d "$sqlDbName" -Q "$addWriterRoleQuery"
    
    connectionString=$(az sql db show-connection-string -s $sqlSrvName -n $sqlDbName -c ado.net \
        | awk -v srch='"' -v repl="" '{sub(srch,repl,$0);print $0}' \
        | awk -v srch='"' -v repl="" '{sub(srch,repl,$0);print $0}' \
        | awk -v srch="<username>" -v repl="$newSqlUserName" '{sub(srch,repl,$0);print $0}' \
        | awk -v srch="<password>" -v repl="$newSqlPass" '{sub(srch,repl,$0);print $0}')
    
    echo 'Setting up new connection string setting with new SQL user'
    temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings DbConnectionString="$connectionString" | jq -r '.')    
    echo Sql Connection String $connectionString
else
    echo 'Skipping SQL User creation'
fi

bash ./service-bus-spin-up.sh
for entity in "${entityTypes[@]}"
do
  for eventType in "${eventTypes[@]}"
  do
    bash ./service-bus-queue-publisher-spin-up.sh $entity $eventType
  done
done

bash ./restart-web-app.sh