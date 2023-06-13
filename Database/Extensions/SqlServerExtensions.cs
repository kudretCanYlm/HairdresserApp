using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Extensions
{
	public static class SqlServerExtensions
	{
		public static void ApplyMigration<T>(this WebApplication app) where T:DbContext
		{
			using (var serviceScope = app.Services.CreateScope())
			{
				serviceScope.ServiceProvider.GetService<T>().Database.Migrate();
			}

			
		}
	}
}
