FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Baixar as dependencias do projeto
COPY ["WS.WorkerExample.csproj", "./"]
RUN dotnet restore "WS.WorkerExample.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet build "WS.WorkerExample.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "WS.WorkerExample.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
ENTRYPOINT ["dotnet", "WS.WorkerExample.dll"]