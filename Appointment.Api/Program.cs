using Appointment.Application.Extensions;
using Appointment.Domain.Extensions;
using Appointment.Infrastructure.Context;
using Appointment.Infrastructure.Extensions;
using Common.Consul;
using Common.Cors;
using Database.Extensions;
using Events.MassTransitOptions;
using Events.Stores.MongoDb;
using Grpc.Auth;
using Grpc.Hairdresser;
using Grpc.HairdresserService;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Swagger;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.UseDomain(typeof(Program));
builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
builder.Services.AddHairdresserGrpc(builder.Configuration["HairdresserUrl"]);
builder.Services.AddHairdresserServiceGrpc(builder.Configuration["HairdresserServiceUrl"]);
builder.Services.AddAppointmentInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentServiceDb")));
builder.Services.AddAppointmentApplication();
builder.Services.AddMongoDbEventStore(builder.Configuration);
builder.Services.AddMyMassTransit(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddMyCors();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger(builder.Configuration);
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

app.ApplyMigration<AppointmentContext>();

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
