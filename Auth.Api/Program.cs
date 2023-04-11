using Auth.Application.Extensions;
using Auth.Domain.Extensions;
using Auth.Infrastructure.Extensions;
using Events.Stores.MongoDb;
using Microsoft.AspNetCore.Builder;
using Redis.OM;
using Swagger;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddMySwagger(builder.Configuration);
		builder.Services.UseDomain(typeof(Program));
		builder.Services.AddMongoDbEventStore(builder.Configuration);
		builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["REDIS_CONNECTION_STRING"]));
		builder.Services.UseAuthApplication();
		builder.Services.UseAuthInfrastructure();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMySwagger(app.Configuration);
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}