# docker-web-app
Example of using ASP.NET Core and MSSQL Server inside docker container.

## Build docker image
```bash
docker build -t maxrok98/docker-webappdb-example .
```

## Run docker container
```bash
docker run -t --name web-api -p 5000:80 -i maxrok98/docker-webappdb-example
```
