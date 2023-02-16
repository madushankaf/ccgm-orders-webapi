
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN adduser --disabled-password --gecos "" myuser

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CCGM.csproj", "./"]
RUN dotnet restore "./CCGM.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CCGM.csproj" -c Debug -o /app/build
FROM build AS publish
RUN dotnet publish "CCGM.csproj" -c Debug -o /app/publish

FROM base AS final

USER myuser
RUN chown -R myuser:myuser /app


WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 9090
ENTRYPOINT ["dotnet", "CCGM.dll", "--urls", "http://0.0.0.0:9090/"]
