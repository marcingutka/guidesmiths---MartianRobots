FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /MartianRobots
COPY . .
RUN dotnet restore
RUN dotnet publish -o /MartianRobots/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /MartianRobots
COPY --from=build /MartianRobots/published-app /MartianRobots
EXPOSE 5000
ENTRYPOINT [ "dotnet", "/MartianRobots/MartianRobots.Api.dll" ]