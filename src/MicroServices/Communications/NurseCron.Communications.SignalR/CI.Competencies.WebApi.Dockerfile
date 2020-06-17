FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./NurseCron.Communications.SignalR .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Communications.SignalR.dll"]
