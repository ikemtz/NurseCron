FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./pub .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.HealthItems.Api.dll"]
