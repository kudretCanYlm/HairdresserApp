using Auth.Domain.Commands.Auth;
using Auth.Domain.Models;
using Auth.Domain.RedisIndex;
using Events.Bus;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System.Reflection;

namespace Auth.Domain.Extensions
{
	public static class DomainExtensions
	{
		public static void UseDomain(this IServiceCollection services,Type startup)
		{
			services.AddHostedService<IndexCreationService>();

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(startup.GetTypeInfo().Assembly));
			services.AddScoped<IMediatorHandler, InMemoryBus>();

			// Domain - Commands
			services.AddScoped<IRequestHandler<LoginCommand, AuthSessionModel>,AuthCommandHandler>();
			services.AddScoped<IRequestHandler<LogoutCommand,ValidationResult>,AuthCommandHandler>();
			services.AddScoped<IRequestHandler<RefreshTokenCommand,AuthSessionModel>,AuthCommandHandler>();

		}
	}
}
