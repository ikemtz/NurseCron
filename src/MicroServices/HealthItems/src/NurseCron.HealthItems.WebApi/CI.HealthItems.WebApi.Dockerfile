FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.HealthItems.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.HealthItems.WebApi.dll"]
