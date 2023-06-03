using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Grpc.Auth;
using Grpc.Hairdresser;
using Grpc.Media;
using HairdresserService.Application.Extensions;
using HairdresserService.Domain.Extensions;
using HairdresserService.Domain.Sagas.HairdresserServiceMedia;
using HairdresserService.GRPC.Services;
using HairdresserService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.WebHost.UseUrls("https://localhost:8069");
builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
builder.Services.AddMediaGrpc(builder.Configuration["MediaUrl"]);
builder.Services.UseDomain(typeof(Program));
builder.Services.AddMyMassTransitStateMachine<HairdresserServiceMediaStateMachine, HairdresserServiceMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_HairdresserServiceMedia);
builder.Services.AddHairdresserServiceInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("HairdresserServiceServiceDb")));
builder.Services.AddHairdresserServiceApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<HairdresserServiceService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
