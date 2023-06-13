using Common.Consul;
using Database.Extensions;
using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Grpc.Media;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using User.Application.Extensions;
using User.Domain.Extensions;
using User.Domain.Sagas.CreateUserMedia;
using User.GRPC.Mapper;
using User.GRPC.Services;
using User.Infrastructure.Context;
using User.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.


//if (builder.Environment.IsDevelopment())
//	builder.WebHost.UseUrls("https://localhost:8067");
builder.Services.UseDomain(typeof(Program));
builder.Services.AddMyMassTransitStateMachine<UserMediaStateMachine, UserMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_UserMedia);
builder.Services.UseUserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserServiceDb")));
builder.Services.UseUserApplication();
builder.Services.AddMediaGrpc(builder.Configuration["MediaUrl"]);
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddAutoMapper(typeof(DtoToProtoModel));
builder.Services.AddGrpc();
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

app.ApplyMigration<UserContext>();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseRouting();
app.UseGrpcMetrics();

app.UseEndpoints(endpoints =>
{
	endpoints.MapMetrics();
});

app.Run();
