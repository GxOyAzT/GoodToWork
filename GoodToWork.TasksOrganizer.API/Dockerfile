#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoodToWork.TasksOrganizer.API/GoodToWork.TasksOrganizer.API.csproj", "GoodToWork.TasksOrganizer.API/"]
COPY ["GoodToWork.Shared.MessageBroker.Infrastructure/GoodToWork.Shared.MessageBroker.Infrastructure.csproj", "GoodToWork.Shared.MessageBroker.Infrastructure/"]
COPY ["GoodToWork.Shared.MessageBroker.Application/GoodToWork.Shared.MessageBroker.Application.csproj", "GoodToWork.Shared.MessageBroker.Application/"]
COPY ["GoodToWork.Shared.MessageBroker.DTOs/GoodToWork.Shared.MessageBroker.DTOs.csproj", "GoodToWork.Shared.MessageBroker.DTOs/"]
COPY ["GoodToWork.TasksOrganizer.Infrastructure/GoodToWork.TasksOrganizer.Infrastructure.csproj", "GoodToWork.TasksOrganizer.Infrastructure/"]
COPY ["GoodToWork.TasksOrganizer.Application/GoodToWork.TasksOrganizer.Application.csproj", "GoodToWork.TasksOrganizer.Application/"]
COPY ["GoodToWork.TasksOrganizer.Domain/GoodToWork.TasksOrganizer.Domain.csproj", "GoodToWork.TasksOrganizer.Domain/"]
COPY ["GoodToWork.Shared.Common.Domain/GoodToWork.Shared.Common.Domain.csproj", "GoodToWork.Shared.Common.Domain/"]
RUN dotnet restore "GoodToWork.TasksOrganizer.API/GoodToWork.TasksOrganizer.API.csproj"
COPY . .
WORKDIR "/src/GoodToWork.TasksOrganizer.API"
RUN dotnet build "GoodToWork.TasksOrganizer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoodToWork.TasksOrganizer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodToWork.TasksOrganizer.API.dll"]