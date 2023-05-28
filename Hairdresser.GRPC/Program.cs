using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Grpc.Media;
using Grpc.User;
using Hairdresser.Application.Extensions;
using Hairdresser.Domain.Extensions;
using Hairdresser.Domain.Sagas.CreateHairdresserMedia;
using Hairdresser.GRPC.Services;
using Hairdresser.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.WebHost.UseUrls("https://localhost:8068");
builder.Services.UseDomain(typeof(Program));
builder.Services.AddUserGrpc(builder.Configuration["UserUrl"]);
builder.Services.AddMediaGrpc(builder.Configuration["MediaUrl"]);
builder.Services.AddMyMassTransitStateMachine<HairdresserMediaStateMachine, HairdresserMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_HairdresserMedia);
builder.Services.UseHairdresserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("HairdresserServiceDb")));
builder.Services.UseHairdresserApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<HairdresserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
