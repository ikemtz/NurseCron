FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./NurseCron.Schedules.OData . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.Schedules.OData.dll"] 
