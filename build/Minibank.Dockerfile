FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ./src/MiniBank.Web/MiniBank.Web.csproj
RUN dotnet test ./src/MiniBank.Core.Tests/MiniBank.Core.Tests.csproj
RUN dotnet build ./src/MiniBank.Web/MiniBank.Web.csproj -c Release -o /app
RUN dotnet publish ./src/MiniBank.Web/MiniBank.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://*:5001;http://*:5000 ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet","MiniBank.Web.dll"]