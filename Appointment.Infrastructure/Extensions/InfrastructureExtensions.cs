using Appointment.Domain.Interfaces;
using Appointment.Infrastructure.Context;
using Appointment.Infrastructure.Repository.Appointment;
using Database.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Infrastructure.Extensions
{
	public static class InfrastructureExtensions
	{
		public static void AddAppointmentInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
		{
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
			services.AddScoped(typeof(IDatabaseFactory<>), typeof(DatabaseFactory<>));

			services.AddScoped<IAppointmentRepository, AppointmentRepository>();
			services.AddDbContext<AppointmentContext>(dbContextOptions);
		}
	}
}
