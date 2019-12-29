FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./IkeMtz.NRSRx.Competencies.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Competencies.OData.dll"]
