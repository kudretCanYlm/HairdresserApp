using Common.Consul;
using Database.Extensions;
using Events.Stores.MongoDb;
using Media.Application.Extensions;
using Media.Domain.Extensions;
using Media.GRPC.Mapper;
using Media.GRPC.Services;
using Media.Infrastructure.Context;
using Media.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
//if (builder.Environment.IsDevelopment())
//	builder.WebHost.UseUrls("https://localhost:8066");

builder.Services.UseDomain(typeof(Program));
builder.Services.UseMediaInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MediaServiceDb")));
builder.Services.UseMediaApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddAutoMapper(typeof(DtoToProtoModel));
builder.Services.AddGrpc();
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

app.ApplyMigration<MediaContext>();
// Configure the HTTP request pipeline.
app.MapGrpcService<MediaService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseRouting();
app.UseGrpcMetrics();

app.UseEndpoints(endpoints =>
{
	endpoints.MapMetrics();
});

app.Run();
