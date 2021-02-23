#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["src/FavoDeMel.Api/FavoDeMel.Api.csproj", "src/FavoDeMel.Api/"]
COPY ["src/FavoDeMel.IoC/FavoDeMel.IoC.csproj", "src/FavoDeMel.IoC/"]
COPY ["src/FavoDeMel.EF.Repository/FavoDeMel.EF.Repository.csproj", "src/FavoDeMel.EF.Repository/"]
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