using Auth.Application.Interfaces.Auth;
using Auth.Application.Mapper;
using Auth.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application.Extensions
{
	public static class ApplicationExtensions
	{
		public static void UseAuthApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ModelToDto));
			services.AddScoped<IAuthAppService, AuthAppService>();
		}
	}
}
