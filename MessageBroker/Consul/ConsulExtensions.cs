using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.Consul
{
	public static class ConsulExtensions
	{
		public static void AddConsul(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IHostedService, ConsulRegisterService>();
			services.Configure<ServiceConfiguration>(configuration.GetSection("ServiceConfiguration"));
			services.Configure<ConsulConfiguration>(configuration.GetSection("Consul"));

			var consulAddress = configuration.GetSection("Consul")["Url"];

			services.AddSingleton<IConsulClient, ConsulClient>(provider =>
				new ConsulClient(config => config.Address = new Uri(consulAddress)));
		}


	}
}
