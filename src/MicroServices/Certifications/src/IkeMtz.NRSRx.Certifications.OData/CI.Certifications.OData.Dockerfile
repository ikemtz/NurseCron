FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./IkeMtz.NRSRx.Certifications.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Certifications.OData.dll"]
