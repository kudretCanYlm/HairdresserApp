#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["User.Api/User.Api.csproj", "User.Api/"]
COPY ["Grpc/Grpc.csproj", "Grpc/"]
COPY ["Logging/Logging.csproj", "Logging/"]
COPY ["Middlewares/Middlewares.csproj", "Middlewares/"]
COPY ["Swagger/Swagger.csproj", "Swagger/"]
COPY ["User.Application/User.Application.csproj", "User.Application/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["User.Domain/User.Domain.csproj", "User.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Filters/Filters.csproj", "Filters/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
COPY ["User.Core/User.Core.csproj", "User.Core/"]
COPY ["User.Infrastructure/User.Infrastructure.csproj", "User.Infrastructure/"]
RUN dotnet restore "User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]