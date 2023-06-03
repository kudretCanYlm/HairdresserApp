using Common.Cors;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

namespace ApiGateway
{
	public class Startup
	{
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMyCors();

			services.AddOcelot();
			
		}
		async public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMyCors();
			app.UseWebSockets();
			await app.UseOcelot();
			
		}
	}
}
