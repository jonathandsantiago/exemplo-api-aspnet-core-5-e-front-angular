#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/FavoDeMel.Api/FavoDeMel.Api.csproj", "src/FavoDeMel.Api/"]
COPY ["src/FavoDeMel.Repository/FavoDeMel.Repository.csproj", "src/FavoDeMel.Repository/"]
COPY ["src/FavoDeMel.Domain/FavoDeMel.Domain.csproj", "src/FavoDeMel.Domain/"]
COPY ["src/FavoDeMel.Service/FavoDeMel.Service.csproj", "src/FavoDeMel.Service/"]
RUN dotnet restore "src/FavoDeMel.Api/FavoDeMel.Api.csproj"
COPY . .
WORKDIR "/src/src/FavoDeMel.Api"
RUN dotnet build "FavoDeMel.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoDeMel.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoDeMel.Api.dll"]