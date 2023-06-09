using Common.Consul;
using Common.Cors;
using Database.Repository.Redis;
using Events.Appointment;
using Events.MassTransitOptions;
using Grpc.Auth;
using Grpc.Hairdresser;
using Grpc.HairdresserService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.HttpOverrides;
using Notification.Api.Hubs;
using Notification.Api.Integrations.Consumers;
using Notification.Api.Interfaces;
using Notification.Api.RedisIndex;
using Notification.Api.Repository;
using Prometheus;
using Redis.OM;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHairdresserGrpc(builder.Configuration["HairdresserUrl"]);
builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
builder.Services.AddHairdresserServiceGrpc(builder.Configuration["HairdresserServiceUrl"]);
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<UserTracker>();
builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["REDIS_CONNECTION_STRING"]));
builder.Services.AddHostedService<IndexCreationService>();
builder.Services.AddScoped(typeof(IRedisBaseRepository<>), typeof(RedisBaseRepository<>));
builder.Services.AddMyMassTransit(builder.Configuration, Assembly.GetExecutingAssembly(),
	(RabbitMqQueues.Appointment_AppointmentCreatedEventQueue, typeof(AppointmentCreatedEventConsumers)),
	(RabbitMqQueues.Appointment_AppointmentApprovedEventQueue, typeof(AppointmentAppovedEventConsumers))
	);

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "AllowReact",
					  build =>
					  {
						  build.WithOrigins(builder.Configuration["SignalRClient"])
						  .AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowCredentials();
					  });
});
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();



app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("AllowReact");

app.UseRouting();
app.UseHttpMetrics();
app.MapMetrics();

app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<NotificationHub>("hubs/notification");
});

app.Run();
