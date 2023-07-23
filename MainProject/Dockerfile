

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MainProject/MainProject.csproj", "MainProject/"]
COPY ["OvetimePolicies/OvetimePolicies.csproj", "OvetimePolicies/"]
RUN dotnet restore "MainProject/MainProject.csproj"
COPY . .
WORKDIR "/src/MainProject"
RUN dotnet build "MainProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MainProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainProject.dll"]