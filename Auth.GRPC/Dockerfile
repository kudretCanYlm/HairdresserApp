#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Auth.GRPC/Auth.GRPC.csproj", "Auth.GRPC/"]
COPY ["Auth.Application/Auth.Application.csproj", "Auth.Application/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["Auth.Infrastructure/Auth.Infrastructure.csproj", "Auth.Infrastructure/"]
COPY ["Grpc/Grpc.csproj", "Grpc/"]
RUN dotnet restore "Auth.GRPC/Auth.GRPC.csproj"
COPY . .
WORKDIR "/src/Auth.GRPC"
RUN dotnet build "Auth.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.GRPC.dll"]