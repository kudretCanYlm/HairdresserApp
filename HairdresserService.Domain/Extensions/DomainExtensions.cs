using Events.Bus;
using Events.HairdresserService;
using Filters.Behaviours;
using FluentValidation;
using FluentValidation.Results;
using HairdresserService.Domain.Commands.HairdresserService;
using HairdresserService.Domain.Events;
using HairdresserService.Domain.Mapper;
using HairdresserService.Domain.Models;
using HairdresserService.Domain.Queries.HairdresserService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System.Reflection;

namespace HairdresserService.Domain.Extensions
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
			services.AddScoped<INotificationHandler<HairdresserServiceActivatedEvent>, HairdresserServiceEventHandler>();
			services.AddScoped<INotificationHandler<HairdresserServiceCreatedEvent>, HairdresserServiceEventHandler>();
			services.AddScoped<INotificationHandler<HairdresserServiceDeactivatedEvent>, HairdresserServiceEventHandler>();
			services.AddScoped<INotificationHandler<HairdresserServiceDeletedEvent>, HairdresserServiceEventHandler>();
			services.AddScoped<INotificationHandler<HairdresserServiceUpdatedEvent>, HairdresserServiceEventHandler>();


			// Domain - Commands
			//test later
			//services.AddScoped(typeof(IRequestHandler<HairdresserServiceCommand, ValidationResult>), typeof(HairdresserServiceCommandHandler));
			services.AddScoped<IRequestHandler<ActivateHairdresserServiceCommand, ValidationResult>, HairdresserServiceCommandHandler>();
			services.AddScoped<IRequestHandler<CreateHairdresserServiceCommand, ValidationResult>, HairdresserServiceCommandHandler>();
			services.AddScoped<IRequestHandler<DeactivateHairdresserServiceCommand, ValidationResult>, HairdresserServiceCommandHandler>();
			services.AddScoped<IRequestHandler<DeleteHairdresserServiceCommand, ValidationResult>, HairdresserServiceCommandHandler>();
			services.AddScoped<IRequestHandler<UpdateHairdresserServiceCommand, ValidationResult>, HairdresserServiceCommandHandler>();

			// Domain - Queries
			//test later
			//services.AddScoped(typeof(IRequestHandler<HairdresserServiceQuery,>), typeof(HairdresserServiceQueryHandler));
			services.AddScoped<IRequestHandler<GetAllHairdresserServicesByHairdresserIdQuery, IEnumerable<HairdresserServiceModel>>, HairdresserServiceQueryHandler>();
			services.AddScoped<IRequestHandler<GetHairdresserServiceByIdQuery, HairdresserServiceModel>, HairdresserServiceQueryHandler>();
			services.AddScoped<IRequestHandler<GetHairdresserServiceByIdAndHairdresserIdQuery, HairdresserServiceModel>, HairdresserServiceQueryHandler>();


		}
	}
}
