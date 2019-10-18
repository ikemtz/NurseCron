FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY ./IkeMtz.NRSRx.Employees.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Employees.OData.dll"]
