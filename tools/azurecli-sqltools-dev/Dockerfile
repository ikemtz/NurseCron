FROM ubuntu:xenial

RUN apt-get -y update && apt-get -y upgrade
RUN apt-get -y install apt-transport-https
RUN apt-get -y install lsb-release
RUN apt-get -y install software-properties-common
RUN apt-get -y install dirmngr
RUN apt-get -y install debconf-utils
RUN apt-get -y install curl
RUN apt-get install -y jq
RUN rm -rf /var/lib/apt/lists/

# adding custom Azure CLI repository
RUN AZ_REPO=$(lsb_release -cs) && \
        echo "deb [arch=amd64] https://packages.microsoft.com/repos/azure-cli/ $AZ_REPO main" \
        | tee /etc/apt/sources.list.d/azure-cli.list

# adding custom MS SQL repository
RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
RUN curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list

# install azure-cli
RUN apt-get update
RUN apt-get install azure-cli

# install SQL Server drivers and tools
RUN apt-get update && ACCEPT_EULA=Y apt-get install -y msodbcsql mssql-tools
RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
RUN /bin/bash -c "source ~/.bashrc"

# install dotnet core sdk
RUN apt-get -y install dotnet-sdk-2.2

RUN apt-get -y install locales
RUN locale-gen en_US.UTF-8
RUN update-locale LANG=en_US.UTF-8
RUN apt-get -y install unzip
RUN apt-get -y install libunwind-dev

RUN mkdir /opt/mssql-tools/bin/sqlpackage/
ENV PATH="/opt/mssql-tools/bin:/opt/mssql-tools/bin/sqlpackage/:${PATH}"
# Link provided on this page:
# https://docs.microsoft.com/en-us/sql/tools/sqlpackage-download?view=sql-server-2017
ADD https://go.microsoft.com/fwlink/?linkid=2069122 /opt/mssql-tools/bin/sqlpackage/sqlpackage.zip


RUN unzip /opt/mssql-tools/bin/sqlpackage/sqlpackage.zip -d /opt/mssql-tools/bin/sqlpackage/
VOLUME /devvol
WORKDIR /devvol