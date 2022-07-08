FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Certifications.OData .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Certifications.OData.dll"]
