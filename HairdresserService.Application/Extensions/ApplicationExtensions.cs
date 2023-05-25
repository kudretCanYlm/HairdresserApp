using HairdresserService.Application.Interfaces.HairdresserService;
using HairdresserService.Application.Mapper;
using HairdresserService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HairdresserService.Application.Extensions
{
	public static class ApplicationExtensions
	{
		public static void AddHairdresserServiceApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ModelToDto), typeof(DtoToCommand));
			services.AddScoped<IHairdresserServiceAppService, HairdresserServiceAppService>();

		}
	}
}
