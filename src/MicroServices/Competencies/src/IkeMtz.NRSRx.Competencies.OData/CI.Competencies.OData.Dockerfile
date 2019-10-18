FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY ./IkeMtz.NRSRx.Competencies.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Competencies.OData.dll"]
