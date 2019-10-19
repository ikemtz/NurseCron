FROM ikemtz/sql_dacpac:latest as sql-temp
ENV SA_PASSWORD=SqlDockerRocks123!

COPY /IkeMtz.NRSRx.Certifications.DB.dacpac /dacpac/db.dacpac
COPY /LocalPublish.publish.xml /dacpac/pub.xml
RUN /opt/mssql/bin/sqlservr & sleep 20 \
    && dotnet /opt/mssql-tools/bin/sqlpackage/sqlpackage.dll /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/db.dacpac /Profile:/dacpac/pub.xml /p:BlockOnPossibleDataLoss=false \
    && sleep 20 \
    && pkill sqlservr && sleep 10 \
    && rm -rf /dacpac

FROM mcr.microsoft.com/mssql/server
LABEL author="@IkeMtz"
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=SqlDockerRocks123!
EXPOSE 1433
COPY --from=sql-temp /var/opt/mssql/data/NRSRx_Primary.ldf /var/opt/mssql/data/NRSRx_Primary.ldf
COPY --from=sql-temp /var/opt/mssql/data/NRSRx_Primary.mdf /var/opt/mssql/data/NRSRx_Primary.mdf
RUN /opt/mssql/bin/sqlservr & sleep 20 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P SqlDockerRocks123! -d Master -Q "CREATE DATABASE[NRSRx]ON(FILENAME='/var/opt/mssql/data/NRSRx_Primary.mdf'),(FILENAME='/var/opt/mssql/data/NRSRx_Primary.ldf')FOR ATTACH"