using Appointment.Application.Extensions;
using Appointment.Domain.Extensions;
using Appointment.Infrastructure.Extensions;
using Events.Stores.MongoDb;
using Grpc.Auth;
using Microsoft.EntityFrameworkCore;
using Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.UseDomain(typeof(Program));
builder.Services.AddAuthGrpc(builder.Configuration["AuthUrl"]);
builder.Services.AddAppointmentInfrastructure(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentServiceDb")));
builder.Services.AddAppointmentApplication();
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

app.UseAuthMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
