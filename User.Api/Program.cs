using Events.Stores.MongoDb;
using Grpc.Auth;
using Microsoft.EntityFrameworkCore;
using Swagger;
using User.Application.Extensions;
using User.Domain.Extensions;
using User.Infrastructure.Extensions;
using Grpc.Media;
using Events.MassTransitOptions;
using User.Domain.Sagas.CreateUserMedia;
using Elastic.Apm.Api;
using Common.Cors;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		//builder.Host.UseSerilog(LoggingExtensions.Configure);
		//Log.Logger = LoggingExtensions.AddMyLogging(builder.Configuration);

		

		// Add services to the container.
		builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
		builder.Services.AddMediaGrpc(builder.Configuration["MediaUrl"]);
		builder.Services.UseDomain(typeof(Program));
		builder.Services.AddMyMassTransitStateMachine<UserMediaStateMachine, UserMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_UserMedia);
		builder.Services.UseUserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserServiceDb")));
		builder.Services.UseUserApplication();
		builder.Services.AddMongoDbEventStore(builder.Configuration);

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddMySwagger(builder.Configuration);

		//add all
		builder.Services.AddMyCors();

		//test
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

		//add all
		app.UseMyCors();

		app.UseAuthMiddleware();

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}