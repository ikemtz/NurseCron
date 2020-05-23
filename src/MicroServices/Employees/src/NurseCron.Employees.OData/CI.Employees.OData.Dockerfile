FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./NurseCron.Employees.OData . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.Employees.OData.dll"] 
