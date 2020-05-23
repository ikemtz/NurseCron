FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./NurseCron.Competencies.OData . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.Competencies.OData.dll"] 
