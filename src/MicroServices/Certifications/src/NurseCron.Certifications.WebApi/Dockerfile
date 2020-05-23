FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./pub . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.Certifications.WebApi.dll"] 
