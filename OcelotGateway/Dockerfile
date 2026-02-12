# Image de base avec le SDK .NET (pour compiler)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copier le fichier .csproj et restaurer les dépendances
COPY OcelotGateway.csproj .
RUN dotnet restore

# Copier tout le reste du code
COPY . .
RUN dotnet publish -c Release -o out

# Image finale plus légère (runtime seulement)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Port utilisé
EXPOSE 8080

# Commande de démarrage
ENTRYPOINT ["dotnet", "OcelotGateway.dll"]
