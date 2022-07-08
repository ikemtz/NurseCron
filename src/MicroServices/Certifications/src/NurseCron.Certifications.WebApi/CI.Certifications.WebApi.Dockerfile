FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY ./NurseCron.Certifications.WebApi .
EXPOSE 80
ENTRYPOINT ["dotnet", "NurseCron.Certifications.WebApi.dll"]
