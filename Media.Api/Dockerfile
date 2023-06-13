#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Media.Api/Media.Api.csproj", "Media.Api/"]
COPY ["Media.Application/Media.Application.csproj", "Media.Application/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["Media.Domain/Media.Domain.csproj", "Media.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Filters/Filters.csproj", "Filters/"]
COPY ["Media.Infrastructure/Media.Infrastructure.csproj", "Media.Infrastructure/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
COPY ["Swagger/Swagger.csproj", "Swagger/"]
RUN dotnet restore "Media.Api/Media.Api.csproj"
COPY . .
WORKDIR "/src/Media.Api"
RUN dotnet build "Media.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Media.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Media.Api.dll"]