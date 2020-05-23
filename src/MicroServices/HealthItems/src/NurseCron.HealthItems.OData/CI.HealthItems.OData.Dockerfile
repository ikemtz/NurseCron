FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./NurseCron.HealthItems.OData . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.HealthItems.OData.dll"] 
