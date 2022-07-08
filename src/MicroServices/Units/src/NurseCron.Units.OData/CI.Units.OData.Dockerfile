FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Units.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Units.OData.dll"]
