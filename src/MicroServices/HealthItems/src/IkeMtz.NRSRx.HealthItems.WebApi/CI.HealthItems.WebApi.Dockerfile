FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app 
COPY ./IkeMtz.NRSRx.HealthItems.WebApi . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "IkeMtz.NRSRx.HealthItems.WebApi.dll"] 
