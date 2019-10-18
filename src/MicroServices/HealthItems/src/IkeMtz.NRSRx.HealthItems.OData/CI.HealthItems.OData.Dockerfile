FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY ./IkeMtz.NRSRx.HealthItems.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.HealthItems.OData.dll"]
