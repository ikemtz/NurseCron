FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./IkeMtz.NRSRx.Employees.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Employees.OData.dll"]
