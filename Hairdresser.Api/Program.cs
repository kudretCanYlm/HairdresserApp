using Common.Consul;
using Common.Cors;
using Database.Extensions;
using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Grpc.Auth;
using Grpc.Media;
using Grpc.User;
using Hairdresser.Application.Extensions;
using Hairdresser.Domain.Extensions;
using Hairdresser.Domain.Sagas.CreateHairdresserMedia;
using Hairdresser.Infrastructure.Context;
using Hairdresser.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.UseDomain(typeof(Program));
builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
builder.Services.AddUserGrpc(builder.Configuration["UserUrl"]);
builder.Services.AddMediaGrpc(builder.Configuration["MediaUrl"]);
builder.Services.AddMyMassTransitStateMachine<HairdresserMediaStateMachine, HairdresserMediaStateInstance>(builder.Configuration, RabbitMqQueues.StateMachine_HairdresserMedia);
builder.Services.UseHairdresserInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("HairdresserDb")));
builder.Services.UseHairdresserApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddMyCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger(builder.Configuration);
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

app.ApplyMigration<HairdresserContext>();

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
