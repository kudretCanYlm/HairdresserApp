#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HairdresserService.GRPC/HairdresserService.GRPC.csproj", "HairdresserService.GRPC/"]
COPY ["Grpc/Grpc.csproj", "Grpc/"]
COPY ["HairdresserService.Application/HairdresserService.Application.csproj", "HairdresserService.Application/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["HairdresserService.Domain/HairdresserService.Domain.csproj", "HairdresserService.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Filters/Filters.csproj", "Filters/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
COPY ["HairdresserService.Infrastructure/HairdresserService.Infrastructure.csproj", "HairdresserService.Infrastructure/"]
RUN dotnet restore "HairdresserService.GRPC/HairdresserService.GRPC.csproj"
COPY . .
WORKDIR "/src/HairdresserService.GRPC"
RUN dotnet build "HairdresserService.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HairdresserService.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HairdresserService.GRPC.dll"]