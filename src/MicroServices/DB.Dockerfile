FROM ikemtz/sql_dacpac:latest as sql-temp
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y

#Copy dacpacs
COPY /Certifications/src/IkeMtz.NRSRx.Certifications.DB/bin/Debug/IkeMtz.NRSRx.Certifications.DB.dacpac /dacpac/certDb.dacpac
COPY /Competencies/src/IkeMtz.NRSRx.Competencies.DB/bin/Debug/IkeMtz.NRSRx.Competencies.DB.dacpac /dacpac/compDb.dacpac
COPY /Employees/src/IkeMtz.NRSRx.Employees.DB/bin/Debug/IkeMtz.NRSRx.Employees.DB.dacpac /dacpac/emplDb.dacpac
COPY /HealthItems/src/IkeMtz.NRSRx.HealthItems.DB/bin/Debug/IkeMtz.NRSRx.HealthItems.DB.dacpac /dacpac/itemDb.dacpac

#Copy publish.xml
COPY /Certifications/src/IkeMtz.NRSRx.Certifications.DB/LocalPublish.publish.xml /dacpac/pub.xml

RUN /opt/mssql/bin/sqlservr & sleep 20 \
    && dotnet /opt/mssql-tools/bin/sqlpackage/sqlpackage.dll /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/certDb.dacpac /Profile:/dacpac/pub.xml /p:BlockOnPossibleDataLoss=false \
    && dotnet /opt/mssql-tools/bin/sqlpackage/sqlpackage.dll /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/compDb.dacpac /Profile:/dacpac/pub.xml /p:BlockOnPossibleDataLoss=false \
    && dotnet /opt/mssql-tools/bin/sqlpackage/sqlpackage.dll /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/emplDb.dacpac /Profile:/dacpac/pub.xml /p:BlockOnPossibleDataLoss=false \
    && dotnet /opt/mssql-tools/bin/sqlpackage/sqlpackage.dll /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/itemDb.dacpac /Profile:/dacpac/pub.xml /p:BlockOnPossibleDataLoss=false \
    && sleep 20 \
    && pkill sqlservr && sleep 10 \
    && rm -rf /dacpac

FROM mcr.microsoft.com/mssql/server
LABEL author="@IkeMtz"
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y
EXPOSE 1433
COPY --from=sql-temp /var/opt/mssql/data/NRSRx_Primary.ldf /var/opt/mssql/data/NRSRx_Primary.ldf
COPY --from=sql-temp /var/opt/mssql/data/NRSRx_Primary.mdf /var/opt/mssql/data/NRSRx_Primary.mdf
RUN /opt/mssql/bin/sqlservr & sleep 20 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P SqlDockerRocks123! -d Master -Q "CREATE DATABASE[NRSRx]ON(FILENAME='/var/opt/mssql/data/NRSRx_Primary.mdf'),(FILENAME='/var/opt/mssql/data/NRSRx_Primary.ldf')FOR ATTACH"