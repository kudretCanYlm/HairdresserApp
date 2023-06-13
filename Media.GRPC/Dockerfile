#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Media.GRPC/Media.GRPC.csproj", "Media.GRPC/"]
COPY ["Media.Application/Media.Application.csproj", "Media.Application/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["Media.Domain/Media.Domain.csproj", "Media.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Filters/Filters.csproj", "Filters/"]
COPY ["Media.Infrastructure/Media.Infrastructure.csproj", "Media.Infrastructure/"]
RUN dotnet restore "Media.GRPC/Media.GRPC.csproj"
COPY . .
WORKDIR "/src/Media.GRPC"
RUN dotnet build "Media.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Media.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Media.GRPC.dll"]