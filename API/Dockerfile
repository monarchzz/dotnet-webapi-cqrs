FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotnetWebAPI/DotnetWebAPI.csproj", "DotnetWebAPI/"]
RUN dotnet restore "DotnetWebAPI/DotnetWebAPI.csproj"
COPY . .
WORKDIR "/src/DotnetWebAPI"
RUN dotnet build "DotnetWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetWebAPI.dll"]
