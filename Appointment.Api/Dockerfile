#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Appointment.Api/Appointment.Api.csproj", "Appointment.Api/"]
COPY ["Application/Appointment.Application.csproj", "Application/"]
COPY ["Appointment.Domain/Appointment.Domain.csproj", "Appointment.Domain/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Events/Events.csproj", "Events/"]
COPY ["Filters/Filters.csproj", "Filters/"]
COPY ["Grpc/Grpc.csproj", "Grpc/"]
COPY ["Appointment.Infrastructure/Appointment.Infrastructure.csproj", "Appointment.Infrastructure/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
COPY ["Swagger/Swagger.csproj", "Swagger/"]
COPY ["MessageBroker/Common.csproj", "MessageBroker/"]
RUN dotnet restore "Appointment.Api/Appointment.Api.csproj"
COPY . .
WORKDIR "/src/Appointment.Api"
RUN dotnet build "Appointment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Appointment.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointment.Api.dll"]