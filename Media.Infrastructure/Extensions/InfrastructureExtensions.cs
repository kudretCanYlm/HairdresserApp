using Database.Infrastructure;
using Media.Domain.Interfaces;
using Media.Infrastructure.Context;
using Media.Infrastructure.Repository.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Infrastructure.Extensions
{
	public static class InfrastructureExtensions
	{
		public static void UseMediaInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
		{
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
			services.AddScoped(typeof(IDatabaseFactory<>), typeof(DatabaseFactory<>));

			services.AddScoped<IMediaRepository, MediaRepository>();
			services.AddDbContext<MediaContext>(dbContextOptions);
		}
	}
}
