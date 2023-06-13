using Common.Consul;
using Common.Cors;
using Database.Extensions;
using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Grpc.Auth;
using Grpc.Hairdresser;
using Grpc.Media;
using HairdresserService.Application.Extensions;
using HairdresserService.Domain.Extensions;
using HairdresserService.Domain.Sagas.HairdresserServiceMedia;
using HairdresserService.Infrastructure.Context;
using HairdresserService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
builder.Services.AddHairdresserGrpc(builder.Configuration["HairdresserUrl"]);
builder.Services.AddMediaGrpc(builder.Configuration["MediaUrl"]);
builder.Services.UseDomain(typeof(Program));
builder.Services.AddMyMassTransitStateMachine<HairdresserServiceMediaStateMachine, HairdresserServiceMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_HairdresserServiceMedia);
builder.Services.AddHairdresserServiceInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("HairdresserServiceServiceDb")));
builder.Services.AddHairdresserServiceApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddMyCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger(builder.Configuration);
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

app.ApplyMigration<HairdresserServiceContext>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMySwagger(app.Configuration);
}

app.UseRouting();
app.UseHttpMetrics();
app.MapMetrics();

app.UseMyCors();

app.UseAuthMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
