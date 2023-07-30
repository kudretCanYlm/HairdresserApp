using Auth.Application.Extensions;
using Auth.Domain.Extensions;
using Auth.GRPC.Mapper;
using Auth.GRPC.Services;
using Auth.Infrastructure.Extensions;
using Common.Consul;
using Events.Stores.MongoDb;
using Prometheus;
using Redis.OM;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		//if (builder.Environment.IsDevelopment())
		//	builder.WebHost.UseUrls("https://localhost:8065");
		
		// Additional configuration is required to successfully run gRPC on macOS.
		// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

		// Add services to the container.
		builder.Services.UseDomain(typeof(Program));
		builder.Services.AddMongoDbEventStore(builder.Configuration);
		builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["REDIS_CONNECTION_STRING"]));
		builder.Services.UseAuthApplication();
		builder.Services.UseAuthInfrastructure();
		builder.Services.AddAutoMapper(typeof(DtoToProtoModel));
		builder.Services.AddGrpc();
		builder.Services.AddConsul(builder.Configuration);

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		app.MapGrpcService<AuthService>();
		app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

		app.UseRouting();
		app.UseGrpcMetrics();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapMetrics();
		});

		app.Run();
	}
}