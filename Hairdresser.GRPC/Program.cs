using Common.Consul;
using Database.Extensions;
using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Grpc.Media;
using Grpc.User;
using Hairdresser.Application.Extensions;
using Hairdresser.Domain.Extensions;
using Hairdresser.Domain.Sagas.CreateHairdresserMedia;
using Hairdresser.GRPC.Services;
using Hairdresser.Infrastructure.Context;
using Hairdresser.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
//if (builder.Environment.IsDevelopment())
//	builder.WebHost.UseUrls("https://localhost:8068");

builder.Services.UseDomain(typeof(Program));
builder.Services.AddUserGrpc(builder.Configuration["UserUrl"]);
builder.Services.AddMediaGrpc(builder.Configuration["MediaUrl"]);
builder.Services.AddMyMassTransitStateMachine<HairdresserMediaStateMachine, HairdresserMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_HairdresserMedia);
builder.Services.UseHairdresserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("HairdresserDb")));
builder.Services.UseHairdresserApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);

builder.Services.AddGrpc();
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

app.ApplyMigration<HairdresserContext>();

// Configure the HTTP request pipeline.
app.MapGrpcService<HairdresserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseRouting();
app.UseGrpcMetrics();

app.UseEndpoints(endpoints =>
{
	endpoints.MapMetrics();
});

app.Run();
