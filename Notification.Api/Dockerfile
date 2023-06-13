#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Notification.Api/Notification.Api.csproj", "Notification.Api/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["Grpc/Grpc.csproj", "Grpc/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
RUN dotnet restore "Notification.Api/Notification.Api.csproj"
COPY . .
WORKDIR "/src/Notification.Api"
RUN dotnet build "Notification.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Api.dll"]