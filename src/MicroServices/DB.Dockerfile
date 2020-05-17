FROM ikemtz/sql_dacpac:latest as sql-temp
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y

#Copy dacpacs
COPY /Certifications/src/IkeMtz.NRSRx.Certifications.DB/bin/Debug/certDb.dacpac /dacpac/certDb.dacpac
COPY /Competencies/src/IkeMtz.NRSRx.Competencies.DB/bin/Debug/compDb.dacpac /dacpac/compDb.dacpac
COPY /Employees/src/IkeMtz.NRSRx.Employees.DB/bin/Debug/emplDb.dacpac /dacpac/emplDb.dacpac
COPY /HealthItems/src/IkeMtz.NRSRx.HealthItems.DB/bin/Debug/hltiDb.dacpac /dacpac/hltiDb.dacpac

#Copy publish.xml
COPY /Certifications/src/IkeMtz.NRSRx.Certifications.DB/LocalPublish.publish.xml /dacpac/pub.xml

RUN /opt/mssql/bin/sqlservr & sleep 20 \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/certDb.dacpac /TargetDatabaseName:certDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/compDb.dacpac /TargetDatabaseName:compDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/emplDb.dacpac /TargetDatabaseName:emplDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/hltiDb.dacpac /TargetDatabaseName:itemDb /p:BlockOnPossibleDataLoss=false \
    && sleep 20 \
    && pkill sqlservr && sleep 10 \
    && sudo rm -rf /dacpac

FROM mcr.microsoft.com/mssql/server
LABEL author="@IkeMtz"
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y
EXPOSE 1433
COPY --from=sql-temp /var/opt/mssql/data/ /var/opt/mssql/data/

USER root
RUN chown -R mssql /var/opt/mssql/data
USER mssql
