using Common.Consul;
using Common.Cors;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

namespace ApiGateway
{
	public class Startup
	{

		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMyCors();

			services.AddOcelot();

			services.AddConsul(configuration);

		}
		async public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMyCors();
			app.UseWebSockets();
			await app.UseOcelot();
			
		}
	}
}
