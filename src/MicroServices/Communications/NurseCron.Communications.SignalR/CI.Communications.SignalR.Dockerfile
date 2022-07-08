FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Communications.SignalR .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Communications.SignalR.dll"]
