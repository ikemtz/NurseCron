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
    && /opt/mssql-tools/bin/sqlpackage/sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/certDb.dacpac /TargetDatabaseName:certDb /p:BlockOnPossibleDataLoss=false \
    && /opt/mssql-tools/bin/sqlpackage/sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/compDb.dacpac /TargetDatabaseName:compDb /p:BlockOnPossibleDataLoss=false \
    && /opt/mssql-tools/bin/sqlpackage/sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/emplDb.dacpac /TargetDatabaseName:emplDb /p:BlockOnPossibleDataLoss=false \
    && /opt/mssql-tools/bin/sqlpackage/sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/itemDb.dacpac /TargetDatabaseName:itemDb /p:BlockOnPossibleDataLoss=false \
    && sleep 20 \
    && pkill sqlservr && sleep 10 \
    && rm -rf /dacpac

FROM mcr.microsoft.com/mssql/server
LABEL author="@IkeMtz"
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y
EXPOSE 1433
COPY --from=sql-temp /var/opt/mssql/data/*.ldf /var/opt/mssql/data/
COPY --from=sql-temp /var/opt/mssql/data/*.mdf /var/opt/mssql/data/
