FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./AutomationAPI/AutomationAPI.csproj", "AutomationAPI/"]
COPY ["./Repository/Repository.csproj", "Repository/"]
COPY ["./Service/Service.csproj", "Service/"]
COPY ["./Core/Core.csproj", "Core/"]

RUN dotnet restore "AutomationAPI/AutomationAPI.csproj"
COPY . .
WORKDIR "/src/AutomationAPI/"
RUN dotnet build "AutomationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutomationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutomationAPI.dll"]
