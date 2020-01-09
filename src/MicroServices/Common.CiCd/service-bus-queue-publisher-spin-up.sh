#!/bin/bash
#

if [ -z $2 ]
then
  echo You must specify an entity and an eventType
  echo Example: bash service-bus-queue-publisher-spin-up.sh Employee Created
  exit 1
fi

  export entity=$1
  export eventType=$2
  echo $entity - $eventType
  export queueSettingName=$entity$eventType'QueConnStr'
  echo $queueSettingName
  # Getting Queue Connection String on Web App 
  export sbConnectionString=$(az webapp config appsettings list --resource-group $appsRgName --name $app1Name \
    | jq '.[] | select(.name=="'$queueSettingName'") | .value' \
    | tr ' ' 'x')
  echo sbConnectionString is $sbConnectionString
  if [ -z $sbConnectionString ]
  then
    # Creating Queue
    echo Creating ServiceBus Queue $eventType
    export queueId=$(az servicebus queue create --name $entity-$eventType \
      --resource-group $serviceBusRgName \
      --namespace-name $serviceBusNamespace \
      --enable-dead-lettering-on-message-expiration true \
      --max-size 5120 --default-message-time-to-live P14D \
      | jq -r '. | .id')    
    echo $eventType Queue Id: ${queueId}

    echo Creating ServiceBus Keys
    export sasKeyName=$entity'MicroService-Publisher';
    export sbRuleId=$(az servicebus queue authorization-rule create --resource-group $serviceBusRgName --namespace-name $serviceBusNamespace --queue-name $entity-$eventType --name $sasKeyName --rights Send | jq -r '. | .id')
    echo $eventType ServiceBus Rule Id: ${sbRuleId}
    export tempConnectionString=$(az servicebus queue authorization-rule keys list --resource-group $serviceBusRgName --namespace-name $serviceBusNamespace --queue-name $entity-$eventType --name $sasKeyName | jq -r '. | .primaryConnectionString')
    echo tempConnectionString $tempConnectionString
    export sbConnectionString=$(echo $tempConnectionString | sed -e 's/;EntityPath=\S*$//')
    echo $eventType Connection String: ${sbConnectionString}
    export queueSettingKVP=$queueSettingName=$sbConnectionString
    echo $queueSettingKVP
    temp=$(az webapp config appsettings set --resource-group $appsRgName --name $app1Name --settings $queueSettingKVP | jq -r '.')

    echo
  else
    echo Skipping creation of ServiceBus keys
  fi
