#!/bin/bash
#
az webapp stop --name $app1Name --resource-group $appsRgName
az webapp start --name $app1Name --resource-group $appsRgName