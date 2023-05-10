using Media.Application.Interfaces.Media;
using Media.Application.Mapper;
using Media.Application.Services.Media;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Application.Extensions
{
	public static class ApplicationExtensions
	{
		public static void UseMediaApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ModelToDto), typeof(DtoToCommand));
			services.AddScoped<IMediaAppService, MediaAppService>();

		}
	}
}
