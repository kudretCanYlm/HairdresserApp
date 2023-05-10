using Events.Bus;
using Events.User;
using Events.User.Address;
using Filters.Behaviours;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System.Reflection;
using User.Domain.Commands.Address;
using User.Domain.Commands.User;
using User.Domain.Events;
using User.Domain.Mapper;
using User.Domain.Models;
using User.Domain.Queries.Address;
using User.Domain.Queries.User;
using User.Domain.Validations.User;

namespace User.Domain.Extensions
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
			services.AddScoped<INotificationHandler<UserCreatedEvent>, UserEventHandler>();
			services.AddScoped<INotificationHandler<UserDeletedEvent>, UserEventHandler>();
			services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
			services.AddScoped<INotificationHandler<UserAddressUpdatedEvent>, AddressEventHandler>();
			services.AddScoped<INotificationHandler<UserAddressDeletedEvent>, AddressEventHandler>();
			services.AddScoped<INotificationHandler<UserAddressCreatedEvent>, AddressEventHandler>();

			// Domain - Commands
			services.AddScoped<IRequestHandler<CreateUserCommand, ValidationResult>, UserCommandHandler>();
			services.AddScoped<IRequestHandler<DeleteUserCommand, ValidationResult>, UserCommandHandler>();
			services.AddScoped<IRequestHandler<UpdateUserCommand, ValidationResult>, UserCommandHandler>();
			services.AddScoped<IRequestHandler<CreateUserAddressCommand,ValidationResult>,AddressCommandHandler>();
			services.AddScoped<IRequestHandler<DeleteUserAddressCommand,ValidationResult>,AddressCommandHandler>();
			services.AddScoped<IRequestHandler<UpdateUserAddressCommand,ValidationResult>,AddressCommandHandler>();

			// Domain - Queries
			services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>,UserQueryHandler>();
			services.AddScoped<IRequestHandler<GetUserByIdQuery, UserModel>, UserQueryHandler>();
			services.AddScoped<IRequestHandler<GetAllUserAddressesQuery, IEnumerable<AddressModel>>,AddressQueryHandler>();
			services.AddScoped<IRequestHandler<GetUserAddressByIdQuery, AddressModel>,AddressQueryHandler>();
			services.AddScoped<IRequestHandler<GetUserAddressesByUserId, IEnumerable<AddressModel>>, AddressQueryHandler>();


		}
	}
}
