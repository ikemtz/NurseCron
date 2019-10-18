FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY ./IkeMtz.NRSRx.Competencies.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Competencies.WebApi.dll"]
