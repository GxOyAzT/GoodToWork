FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../GoodToWork.UserSerice.API/GoodToWork.UserSerice.API.csproj", "../GoodToWork.UserSerice.API/"]
RUN dotnet restore "../GoodToWork.UserSerice.API/GoodToWork.UserSerice.API.csproj"
COPY . .
WORKDIR "/src/../GoodToWork.UserSerice.API"
RUN dotnet build "GoodToWork.UserSerice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoodToWork.UserSerice.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodToWork.UserSerice.API.dll"]