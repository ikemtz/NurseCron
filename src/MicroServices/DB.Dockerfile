FROM ikemtz/sql_dacpac:latest as sql-temp
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y

#Copy dacpacs
COPY /Certifications/src/NurseCron.Certifications.DB/bin/Debug/*.dacpac /dacpac/
COPY /Competencies/src/NurseCron.Competencies.DB/bin/Debug/*.dacpac /dacpac/
COPY /Employees/src/NurseCron.Employees.DB/bin/Debug/*.dacpac /dacpac/
COPY /HealthItems/src/NurseCron.HealthItems.DB/bin/Debug/*.dacpac /dacpac/
COPY /Units/src/NurseCron.Units.DB/bin/Debug/*.dacpac /dacpac/
COPY /Schedules/src/NurseCron.Schedules.DB/bin/Debug/*.dacpac /dacpac/


#Copy publish.xml
COPY /Certifications/src/NurseCron.Certifications.DB/LocalPublish.publish.xml /dacpac/pub.xml

RUN /opt/mssql/bin/sqlservr & sleep 20 \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/certDb.dacpac /TargetDatabaseName:certDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/compDb.dacpac /TargetDatabaseName:compDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/emplDb.dacpac /TargetDatabaseName:emplDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/hltiDb.dacpac /TargetDatabaseName:hltiDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/unitDb.dacpac /TargetDatabaseName:unitDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/schdDb.dacpac /TargetDatabaseName:schdDb /p:BlockOnPossibleDataLoss=false \
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
