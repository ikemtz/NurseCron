FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Competencies.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Competencies.OData.dll"]
