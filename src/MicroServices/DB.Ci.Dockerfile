FROM ikemtz/sql_dacpac:latest as sql-temp
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y

#Copy dacpacs
COPY /*.dacpac /dacpac/

RUN sqlservr & sleep 20 \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/certDb.dacpac /TargetDatabaseName:certDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/compDb.dacpac /TargetDatabaseName:compDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/emplDb.dacpac /TargetDatabaseName:emplDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/hltiDb.dacpac /TargetDatabaseName:hltiDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/unitDb.dacpac /TargetDatabaseName:unitDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/schdDb.dacpac /TargetDatabaseName:schdDb /p:BlockOnPossibleDataLoss=false \
    \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/certDb.dacpac /TargetDatabaseName:nurseCronDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/compDb.dacpac /TargetDatabaseName:nurseCronDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/emplDb.dacpac /TargetDatabaseName:nurseCronDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/hltiDb.dacpac /TargetDatabaseName:nurseCronDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/unitDb.dacpac /TargetDatabaseName:nurseCronDb /p:BlockOnPossibleDataLoss=false \
    && sqlpackage /Action:Publish /TargetServerName:localhost /TargetUser:SA /TargetPassword:$SA_PASSWORD /SourceFile:/dacpac/schdDb.dacpac /TargetDatabaseName:nurseCronDb /p:BlockOnPossibleDataLoss=false \
    \
    && sleep 20 \
    && pkill sqlservr && sleep 10 \
    && sudo rm -rf /dacpac

FROM mcr.microsoft.com/mssql/server:2019-latest
LABEL author="@IkeMtz"
ENV SA_PASSWORD=SqlDockerRocks123! \
    ACCEPT_EULA=Y
EXPOSE 1433
COPY --from=sql-temp /var/opt/mssql/data/ /var/opt/mssql/data/

USER root
RUN chown -R mssql /var/opt/mssql/data
USER mssql
