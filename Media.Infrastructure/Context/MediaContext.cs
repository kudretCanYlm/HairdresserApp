using Database.Extensions;
using Database.Infrastructure;
using FluentValidation.Results;
using Media.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;
using NetDevPack.Messaging;

namespace Media.Infrastructure.Context
{
	public class MediaContext : DbContext, IBaseDbContext
	{
		private readonly IMediatorHandler mediatorHandler;

		public MediaContext(DbContextOptions<MediaContext> options, IMediatorHandler mediatorHandler) : base(options)
		{
			this.mediatorHandler = mediatorHandler;
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			ChangeTracker.AutoDetectChangesEnabled = false;
		}

		public DbSet<MediaModel> Media { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//dont reflect to database
			modelBuilder.Ignore<ValidationResult>();
			modelBuilder.Ignore<Event>();

			base.OnModelCreating(modelBuilder);
		}

		public async Task<bool> Commit()
		{
			//await 
			mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
			var success = await SaveChangesAsync() > 0;

			return success;
		}

		public Task BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public Task RollBack()
		{
			throw new NotImplementedException();
		}
	}
}
