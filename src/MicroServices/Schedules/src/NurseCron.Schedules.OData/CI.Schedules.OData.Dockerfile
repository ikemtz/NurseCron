FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Schedules.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Schedules.OData.dll"]
