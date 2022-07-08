FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Employees.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Employees.WebApi.dll"]
