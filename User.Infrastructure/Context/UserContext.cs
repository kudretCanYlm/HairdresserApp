using Database.Infrastructure;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using User.Domain.Models;

namespace User.Infrastructure.Context
{
	public class UserContext : DbContext, IBaseDbContext
	{
		private readonly IMediatorHandler mediatorHandler;

		public UserContext(DbContextOptions<UserContext> options, IMediatorHandler mediatorHandler) : base(options)
		{
			this.mediatorHandler = mediatorHandler;
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			ChangeTracker.AutoDetectChangesEnabled = false;
		}



		public DbSet<UserModel> User { get; set; }
		public DbSet<AddressModel> Address { get; set; }

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

	public static class MediatorExtension
	{
		public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
		{
			var domainEntities = ctx.ChangeTracker
				.Entries<Entity>()
				.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

			var domainEvents = domainEntities
				.SelectMany(x => x.Entity.DomainEvents)
				.ToList();

			domainEntities.ToList()
				.ForEach(entity => entity.Entity.ClearDomainEvents());

			var tasks = domainEvents
				.Select(async (domainEvent) =>
				{
					await mediator.PublishEvent(domainEvent);
				});

			await Task.WhenAll(tasks);
		}
	}
}
