#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Hairdresser.GRPC/Hairdresser.GRPC.csproj", "Hairdresser.GRPC/"]
COPY ["Hairdresser.Application/Hairdresser.Application.csproj", "Hairdresser.Application/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["Grpc/Grpc.csproj", "Grpc/"]
COPY ["Hairdresser.Domain/Hairdresser.Domain.csproj", "Hairdresser.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Filters/Filters.csproj", "Filters/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
COPY ["Hairdresser.Infrastructure/Hairdresser.Infrastructure.csproj", "Hairdresser.Infrastructure/"]
RUN dotnet restore "Hairdresser.GRPC/Hairdresser.GRPC.csproj"
COPY . .
WORKDIR "/src/Hairdresser.GRPC"
RUN dotnet build "Hairdresser.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hairdresser.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hairdresser.GRPC.dll"]