using Database.Extensions;
using Database.Infrastructure;
using FluentValidation.Results;
using HairdresserService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;
using NetDevPack.Messaging;

namespace HairdresserService.Infrastructure.Context
{
	public class HairdresserServiceContext : DbContext, IBaseDbContext
	{
		private readonly IMediatorHandler mediatorHandler;

		public HairdresserServiceContext(DbContextOptions<HairdresserServiceContext> options, IMediatorHandler mediatorHandler) : base(options)
		{
			this.mediatorHandler = mediatorHandler;
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			ChangeTracker.AutoDetectChangesEnabled = false;
		}

		public DbSet<HairdresserServiceModel> HairdresserService { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//dont reflect to database
			modelBuilder.Ignore<ValidationResult>();
			modelBuilder.Ignore<Event>();

			base.OnModelCreating(modelBuilder);
		}

		public Task BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Commit()
		{
			//await 
			mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
			var success = await SaveChangesAsync() > 0;

			return success;
		}

		public Task RollBack()
		{
			throw new NotImplementedException();
		}
	}
}
