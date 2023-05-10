using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;
using Database.Entity;

namespace Database.Extensions
{
	public static class MediatorExtensions
	{
		public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
		{
			var domainEntities = ctx.ChangeTracker
				.Entries<BaseEntity>()
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
