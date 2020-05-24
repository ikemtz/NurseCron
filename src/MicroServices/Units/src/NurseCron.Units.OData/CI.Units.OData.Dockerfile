FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./NurseCron.Certifications.OData . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.Units.OData.dll"] 
