using Events.Stores.EfCore;
using Events.Stores.MongoDb;
using Grpc.Auth;
using Grpc.Auth.ClientServices;
using Grpc.Auth.Protos;
using Microsoft.EntityFrameworkCore;
using Swagger;
using User.Application.Extensions;
using User.Domain.Extensions;
using User.Infrastructure.Extensions;
using Serilog;
using MEES.Infrastructure.Logging;
using Microsoft.Extensions.Logging.Console;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Host.UseSerilog(LoggingExtensions.Configure);
		//Log.Logger = LoggingExtensions.AddMyLogging(builder.Configuration);


		// Add services to the container.
		builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
		builder.Services.UseDomain(typeof(Program));

		builder.Services.UseUserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserServiceDb")));
		builder.Services.UseUserApplication();
		builder.Services.AddMongoDbEventStore(builder.Configuration);

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddMySwagger(builder.Configuration);
		
		var app = builder.Build();

		//var loggerFactory = LoggerFactory.Create(builder =>
		//{
		//	builder.AddSimpleConsole(i => i.ColorBehavior = LoggerColorBehavior.Disabled);
		//});

		//app.UseMyLogging(builder.Configuration,loggerFactory);
		
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMySwagger(app.Configuration);
		}

		//app.UseAuthMiddleware();

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}