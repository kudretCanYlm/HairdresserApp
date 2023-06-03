using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Media.Api.Integrations.Consumers;
using Media.Application.Extensions;
using Media.Domain.Extensions;
using Media.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Swagger;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.UseDomain(typeof(Program));
builder.Services.UseMediaInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MediaServiceDb")));
builder.Services.UseMediaApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddMyMassTransit(builder.Configuration, Assembly.GetExecutingAssembly(),
	(RabbitMqQueues.Media_MediaCreatedEventQueue, typeof(MediaCreatedEventConsumer)),
	(RabbitMqQueues.Media_MediaDeletedEventQueue, typeof(MediaDeletedEventConsumer)),
	(RabbitMqQueues.Media_MediaUpdatedEventQueue, typeof(MediaUpdatedEventConsumer)));

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
