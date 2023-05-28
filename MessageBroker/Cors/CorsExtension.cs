using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Cors
{
	public static class CorsExtension
	{
		public static void AddMyCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(name: "AllowOrigin",
					builder =>
					{
						builder.WithOrigins("*")
											.AllowAnyHeader()
											.AllowAnyMethod();
					});
			});
		}

		public static void UseMyCors(this IApplicationBuilder app)
		{
			app.UseCors("AllowOrigin");
		}
	}
}
