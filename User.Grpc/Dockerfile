#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["User.Grpc/User.GRPC.csproj", "User.Grpc/"]
COPY ["Grpc/Grpc.csproj", "Grpc/"]
COPY ["User.Application/User.Application.csproj", "User.Application/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["User.Domain/User.Domain.csproj", "User.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Filters/Filters.csproj", "Filters/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
COPY ["User.Infrastructure/User.Infrastructure.csproj", "User.Infrastructure/"]
RUN dotnet restore "User.Grpc/User.GRPC.csproj"
COPY . .
WORKDIR "/src/User.Grpc"
RUN dotnet build "User.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.GRPC.dll"]