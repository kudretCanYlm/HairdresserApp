using Database.PaggingAndFilter;
using Events.Bus;
using Events.Hairdresser;
using Filters.Behaviours;
using FluentValidation;
using FluentValidation.Results;
using Hairdresser.Domain.Commands.Hairdresser;
using Hairdresser.Domain.Events;
using Hairdresser.Domain.Mapper;
using Hairdresser.Domain.Models;
using Hairdresser.Domain.Queries.Hairdresser;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System.Reflection;

namespace Hairdresser.Domain.Extensions
{
	public static class DomainExtensions
	{
		public static void UseDomain(this IServiceCollection services, Type startup)
		{
			services.AddAutoMapper(typeof(DomainToCommandProfile), typeof(EventToCommandProfile));
			services.AddScoped<IMediatorHandler, InMemoryBus>();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(startup.GetTypeInfo().Assembly));

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			// Domain - Events
			services.AddScoped<INotificationHandler<HairdresserCreatedEvent>, HairdresserEventHandler>();
			services.AddScoped<INotificationHandler<HairdresserDeletedEvent>, HairdresserEventHandler>();
			services.AddScoped<INotificationHandler<HairdresserUpdatedEvent>, HairdresserEventHandler>();

			// Domain - Commands
			services.AddScoped<IRequestHandler<CreateHairdresserCommand, ValidationResult>, HairdresserCommandHandler>();
			services.AddScoped<IRequestHandler<DeleteHairdresserCommand, ValidationResult>, HairdresserCommandHandler>();
			services.AddScoped<IRequestHandler<UpdateHairdresserCommand, ValidationResult>, HairdresserCommandHandler>();

			// Domain - Queries
			services.AddScoped<IRequestHandler<GetAllHairdressersQuery, IEnumerable<HairdresserModel>>, HairdresserQueryHandler>();
			services.AddScoped<IRequestHandler<GetHairdresserByIdQuery, HairdresserModel>, HairdresserQueryHandler>();
			services.AddScoped<IRequestHandler<GetAllHairdresserByFilterQuery, IPagedList<HairdresserModel>>, HairdresserQueryHandler>();
			services.AddScoped<IRequestHandler<CheckHairdresserIdAndUserIdQuery, bool>, HairdresserQueryHandler>();


		}
	}
}
