using Events.Stores.EfCore;
using Events.Stores.MongoDb;
using Microsoft.EntityFrameworkCore;
using Swagger;
using User.Application.Extensions;
using User.Domain.Extensions;
using User.Infrastructure.Extensions;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.UseDomain(typeof(Program));
		
		builder.Services.UseUserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserServiceDb")));
		builder.Services.UseUserApplication();
		//will change add mongo db
		builder.Services.AddMongoDbEventStore(builder.Configuration);

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddMySwagger(builder.Configuration);

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