FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./NurseCron.Competencies.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Competencies.WebApi.dll"]
