using Events.Bus;
using Events.Media;
using Filters.Behaviours;
using FluentValidation;
using FluentValidation.Results;
using Media.Domain.Commands.Media;
using Media.Domain.Events;
using Media.Domain.Mapper;
using Media.Domain.Models;
using Media.Domain.Queries.Media;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System.Reflection;

namespace Media.Domain.Extensions
{
	public static class DomainExtensions
	{
		public static void UseDomain(this IServiceCollection services,Type startup)
		{
			services.AddAutoMapper(typeof(DomainToCommandProfile), typeof(EventToCommandProfile));
			services.AddScoped<IMediatorHandler, InMemoryBus>();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(startup.GetTypeInfo().Assembly));

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			// Domain - Events
			services.AddScoped<INotificationHandler<MediaCreatedEvent>, MediaEventHandler>();
			services.AddScoped<INotificationHandler<MediaDeletedEvent>, MediaEventHandler>();
			services.AddScoped<INotificationHandler<MediaUpdatedEvent>, MediaEventHandler>();

			// Domain - Commands
			services.AddScoped<IRequestHandler<CreateMediaCommand, ValidationResult>, MediaCommandHandler>();
			services.AddScoped<IRequestHandler<DeleteMediaCommand, ValidationResult>, MediaCommandHandler>();
			services.AddScoped<IRequestHandler<UpdateMediaCommand, ValidationResult>, MediaCommandHandler>();

			// Domain - Queries
			services.AddScoped<IRequestHandler<GetAllMediasQuery, IEnumerable<MediaModel>>, MediaQueryHandler>();
			services.AddScoped<IRequestHandler<GetMediaByImageOwnerIdAndTypeQuery, MediaModel>, MediaQueryHandler>();
			services.AddScoped<IRequestHandler<GetMediaListByImageOwnerIdAndTypeQuery, IEnumerable<MediaModel>>, MediaQueryHandler>();
			services.AddScoped<IRequestHandler<GetMediaCountByImageOwnerIdAndTypeQuery, int>, MediaQueryHandler>();
			services.AddScoped<IRequestHandler<IsMediaAvailableQuery, bool>, MediaQueryHandler>();
		}
	}
}
