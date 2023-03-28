using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Infrastructure;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;
using NetDevPack.Mediator;
using NetDevPack.Messaging;

namespace Database
{
	//public static class MediatorExtension
	//{
	//	public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
	//	{
	//		var domainEntities = ctx.ChangeTracker
	//			.Entries<Entity>()
	//			.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

	//		var domainEvents = domainEntities
	//			.SelectMany(x => x.Entity.DomainEvents).ToList();

	//		domainEntities.ToList()
	//			.ForEach(entity => entity.Entity.ClearDomainEvents());

	//		var tasks = domainEvents
	//			.Select(async (domainEvent) => {
	//				await mediator.PublishEvent(domainEvent);
	//			});

	//		await Task.WhenAll(tasks);
	//	}
	//}
}
