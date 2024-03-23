FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Adapters/Driver/WorkTracker.API/WorkTracker.API.csproj", "Adapters/Driver/WorkTracker.API/"]
COPY ["Core/UseCase/WorkTracker.Clock.UseCase/WorkTracker.Clock.UseCase.csproj", "Core/UseCase/WorkTracker.Clock.UseCase/"]
COPY ["Core/Domain/WorkTracker.Clock.Domain/WorkTracker.Clock.Domain.csproj", "Core/Domain/WorkTracker.Clock.Domain/"]
COPY ["Core/Domain/WorkTracker.Domain.Core/WorkTracker.Domain.Core.csproj", "Core/Domain/WorkTracker.Domain.Core/"]
COPY ["Core/Data/WorkTracker.Data.Core/WorkTracker.Data.Core.csproj", "Core/Data/WorkTracker.Data.Core/"]
COPY ["Adapters/Driven/WorkTracker.Gateways.MySQL/WorkTracker.Gateways.MySQL.csproj", "Adapters/Driven/WorkTracker.Gateways.MySQL/"]
COPY ["Adapters/Driven/WorkTracker.Gateways.Notification/WorkTracker.Gateways.Notification.csproj", "Adapters/Driven/WorkTracker.Gateways.Notification/"]
COPY ["Adapters/Driven/WorkTracker.Gateways.Http/WorkTracker.Gateways.Http.csproj", "Adapters/Driven/WorkTracker.Gateways.Http/"]

RUN dotnet restore "Adapters/Driver/WorkTracker.API/WorkTracker.API.csproj"
COPY . .
WORKDIR "/src/Adapters/Driver/WorkTracker.API"
RUN dotnet build "WorkTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkTracker.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkTracker.API.dll"]