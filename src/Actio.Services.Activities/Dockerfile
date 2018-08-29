FROM microsoft/dotnet:2.1-aspnetcore-runtime
ENV ASPNETCORE_URLS http://*:5050
ENV ASPNETCORE_ENVIRONMENT Docker
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENTRYPOINT dotnet Actio.Services.Activities.dll