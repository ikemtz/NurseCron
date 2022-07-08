FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Competencies.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Competencies.WebApi.dll"]
