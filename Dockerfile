# docker build -t kritner/todo-web-api .
# heroku container:push web
# heroku container:release web

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./Kritner.TodoBackend.WebApi/Kritner.TodoBackend.WebApi.csproj ./Kritner.TodoBackend.WebApi/Kritner.TodoBackend.WebApi.csproj

WORKDIR /app/Kritner.TodoBackend.WebApi

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out 

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Kritner.TodoBackend.WebApi.dll"]