#!/bin/bash
#
echo Create ServiceBus Resource Group $serviceBusRgName
export serviceBusRgId=$(az group create --location centralus --name $serviceBusRgName | jq -r '. | .id')
echo ServiceBus Resource Group Id: ${serviceBusRgId}
echo

echo Create ServiceBus $serviceBusNamespace
export serviceBusId=$(az servicebus namespace create --resource-group $serviceBusRgName --name $serviceBusNamespace --location centralus --sku Basic | jq -r '. | .id')
echo ServiceBus Id: ${serviceBusId}
echo