FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NetGpt.Api/NetGpt.Api.csproj", "NetGpt.Api/"]
RUN dotnet restore "NetGpt.Api/NetGpt.Api.csproj"
COPY . .
WORKDIR "/src/NetGpt.Api"
RUN dotnet build "NetGpt.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetGpt.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetGpt.Api.dll"]
