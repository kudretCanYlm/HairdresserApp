using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Microsoft.EntityFrameworkCore;
using User.Application.Extensions;
using User.Domain.Extensions;
using User.Domain.Sagas.CreateUserMedia;
using User.GRPC.Mapper;
using User.GRPC.Services;
using User.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.WebHost.UseUrls("https://localhost:8067");
builder.Services.UseDomain(typeof(Program));
builder.Services.AddMyMassTransitStateMachine<UserMediaStateMachine, UserMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_UserMedia);
builder.Services.UseUserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserServiceDb")));
builder.Services.UseUserApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddAutoMapper(typeof(DtoToProtoModel));

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
