#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=123456qQ!
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433

RUN apt-get update
RUN apt-get install apt-utils -y

RUN apt-get install sudo wget curl gnupg gnupg1 gnupg2 -y
RUN apt-get install software-properties-common systemd vim -y
RUN wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -

RUN add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019.list)"
RUN curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
RUN apt-get update
RUN apt-get install -y mssql-server
RUN apt-get install mssql-tools unixodbc-dev -y

RUN /opt/mssql/bin/mssql-conf set hadr.hadrenabled  1
RUN /opt/mssql/bin/mssql-conf set sqlagent.enabled true

WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
RUN dotnet restore "WebApplication1/WebApplication1.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY entry.sh entry.sh
CMD ["bash", "/app/entry.sh"]