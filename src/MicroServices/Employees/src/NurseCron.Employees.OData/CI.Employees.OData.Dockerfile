FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Employees.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Employees.OData.dll"]
