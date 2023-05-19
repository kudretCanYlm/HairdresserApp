using Appointment.Application.Interfaces.Appointment;
using Appointment.Application.Mapper;
using Appointment.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Application.Extensions
{
	public static class ApplicationExtensions
	{
		public static void AddAppointmentApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ModelToDto), typeof(DtoToCommand));
			services.AddScoped<IAppointmentAppService, AppointmentAppService>();
		}
	}
}
