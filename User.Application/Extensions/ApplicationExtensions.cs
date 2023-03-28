using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces.Address;
using User.Application.Interfaces.User;
using User.Application.Mapper;
using User.Application.Services;

namespace User.Application.Extensions
{
	public static class ApplicationExtensions
	{
		public static void UseUserApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(CommandToDto), typeof(DtoToCommand));
			services.AddScoped<IUserAppService,UserAppService>();
			services.AddScoped<IAddressAppService, AddressAppService>();

		}
	}
}
