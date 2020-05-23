FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./NurseCron.Employees.WebApi . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.Employees.WebApi.dll"] 
