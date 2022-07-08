FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.HealthItems.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.HealthItems.OData.dll"]
