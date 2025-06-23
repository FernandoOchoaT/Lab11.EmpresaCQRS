# 🔧 Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos del proyecto
COPY ["Lab11.API/Lab11.API.csproj", "Lab11.API/"]
COPY ["Lab11.Application/Lab11.Application.csproj", "Lab11.Application/"]
COPY ["Lab11.Domain/Lab11.Domain.csproj", "Lab11.Domain/"]
COPY ["Lab11.Infrastructure/Lab11.Infrastructure.csproj", "Lab11.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "Lab11.API/Lab11.API.csproj"

# Copiar todo el código
COPY . .

# Build y publish
WORKDIR "/src/Lab11.API"
RUN dotnet publish "Lab11.API.csproj" -c Release -o /app/publish

# 🚀 Etapa final: Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Punto de entrada
ENTRYPOINT ["dotnet", "Lab11.API.dll"]
