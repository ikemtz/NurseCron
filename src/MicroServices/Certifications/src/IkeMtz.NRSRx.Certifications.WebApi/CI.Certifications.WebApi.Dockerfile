FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY ./IkeMtz.NRSRx.Certifications.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Certifications.WebApi.dll"]
