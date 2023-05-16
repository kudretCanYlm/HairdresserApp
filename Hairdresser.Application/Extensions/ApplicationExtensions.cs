using Hairdresser.Application.Interfaces.Hairdresser;
using Hairdresser.Application.Mapper;
using Hairdresser.Application.Services.Hairdresser;
using Microsoft.Extensions.DependencyInjection;

namespace Hairdresser.Application.Extensions
{
	public static class ApplicationExtensions
	{
		public static void UseHairdresserApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ModelToDto), typeof(DtoToCommand));
			services.AddScoped<IHairdresserAppService, HairdresserAppService>();

		}
	}
}
