FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Lab11.API/Lab11.API.csproj", "Lab11.API/"]
COPY ["Lab11.Application/Lab11.Application.csproj", "Lab11.Application/"]
COPY ["Lab11.Domain/Lab11.Domain.csproj", "Lab11.Domain/"]
COPY ["Lab11.Infrastructure/Lab11.Infrastructure.csproj", "Lab11.Infrastructure/"]

RUN dotnet restore "Lab11.API/Lab11.API.csproj"

COPY . .
WORKDIR "/src/Lab11.API"
RUN dotnet publish "Lab11.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Lab11.API.dll"]
