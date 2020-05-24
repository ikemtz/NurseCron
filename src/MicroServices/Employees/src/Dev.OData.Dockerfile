FROM mcr.microsoft.com/dotnet/core/sdk  as build
WORKDIR /app
ENV domain=Employees

COPY . ./
RUN cd NurseCron.$domain.OData/ \
    && dotnet restore \
    && dotnet publish -c Release -o ../pub


FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app 
COPY --from=build /app/pub . 
EXPOSE 80 
ENTRYPOINT ["dotnet", "NurseCron.Employees.OData.dll"] 
