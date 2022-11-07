
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY AutomationAPI.csproj AutomationAPI/
COPY . . 
COPY "./Repository/*.csproj" "./Repository/"
COPY "./Service/*.csproj" "./Service/"


RUN dotnet restore "AutomationAPI/AutomationAPI.csproj"
COPY . .
WORKDIR "/src/AutomationAPI"

RUN dotnet build "AutomationAPI.csproj" -c Release -o /app/build



FROM build AS publish
RUN dotnet publish "AutomationAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutomationAPI.dll"]