using Events.Stores.MongoDb;
using Media.Application.Extensions;
using Media.Domain.Extensions;
using Media.GRPC.Mapper;
using Media.GRPC.Services;
using Media.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.WebHost.UseUrls("https://localhost:8066");
builder.Services.UseDomain(typeof(Program));
builder.Services.UseMediaInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MediaServiceDb")));
builder.Services.UseMediaApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddAutoMapper(typeof(DtoToProtoModel));
builder.Services.AddGrpc();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<MediaService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
