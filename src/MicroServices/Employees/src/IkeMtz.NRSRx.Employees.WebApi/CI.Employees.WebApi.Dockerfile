FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY ./IkeMtz.NRSRx.Employees.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.Employees.WebApi.dll"]
