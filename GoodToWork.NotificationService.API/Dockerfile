FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoodToWork.NotificationService.API/GoodToWork.NotificationService.API.csproj", "GoodToWork.NotificationService.API/"]
COPY ["GoodToWork.NotificationService.Infrastructure.Emailer/GoodToWork.NotificationService.Infrastructure.Emailer.csproj", "GoodToWork.NotificationService.Infrastructure.Emailer/"]
COPY ["GoodToWork.NotificationService.Application/GoodToWork.NotificationService.Application.csproj", "GoodToWork.NotificationService.Application/"]
COPY ["GoodToWork.NotificationService.Domain/GoodToWork.NotificationService.Domain.csproj", "GoodToWork.NotificationService.Domain/"]
COPY ["GoodToWork.Shared.MessageBroker.Application/GoodToWork.Shared.MessageBroker.Application.csproj", "GoodToWork.Shared.MessageBroker.Application/"]
COPY ["GoodToWork.Shared.MessageBroker.DTOs/GoodToWork.Shared.MessageBroker.DTOs.csproj", "GoodToWork.Shared.MessageBroker.DTOs/"]
COPY ["GoodToWork.Shared.MessageBroker.Infrastructure/GoodToWork.Shared.MessageBroker.Infrastructure.csproj", "GoodToWork.Shared.MessageBroker.Infrastructure/"]
COPY ["GoodToWork.NotificationService.Infrastructure.Persistance/GoodToWork.NotificationService.Infrastructure.Persistance.csproj", "GoodToWork.NotificationService.Infrastructure.Persistance/"]
RUN dotnet restore "GoodToWork.NotificationService.API/GoodToWork.NotificationService.API.csproj"
COPY . .
WORKDIR "/src/GoodToWork.NotificationService.API"
RUN dotnet build "GoodToWork.NotificationService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoodToWork.NotificationService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodToWork.NotificationService.API.dll"]
