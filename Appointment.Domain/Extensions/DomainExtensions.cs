using Appointment.Domain.Commands.Appointment;
using Appointment.Domain.Events;
using Appointment.Domain.Mapper;
using Appointment.Domain.Models;
using Appointment.Domain.Queries.Appointment;
using Events.Appointment;
using Events.Bus;
using Filters.Behaviours;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System.Reflection;

namespace Appointment.Domain.Extensions
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
			services.AddScoped<INotificationHandler<AppointmentApprovedEvent>, AppointmentEventHandler>();
			services.AddScoped<INotificationHandler<AppointmentCanceledEvent>, AppointmentEventHandler>();
			services.AddScoped<INotificationHandler<AppointmentCompletedEvent>, AppointmentEventHandler>();
			services.AddScoped<INotificationHandler<AppointmentCreatedEvent>, AppointmentEventHandler>();
			services.AddScoped<INotificationHandler<AppointmentDeniedEvent>, AppointmentEventHandler>();
			services.AddScoped<INotificationHandler<AppointmentInProcessedEvent>, AppointmentEventHandler>();
			services.AddScoped<INotificationHandler<AppointmentUpdatedEvent>, AppointmentEventHandler>();

			// Domain - Commands
			services.AddScoped<IRequestHandler<ApproveAppointmentCommand, ValidationResult>, AppointmentCommandHandler>();
			services.AddScoped<IRequestHandler<CancelAppointmentCommand, ValidationResult>, AppointmentCommandHandler>();
			services.AddScoped<IRequestHandler<CompleteAppointmentCommand, ValidationResult>, AppointmentCommandHandler>();
			services.AddScoped<IRequestHandler<CreateAppointmentCommand, ValidationResult>, AppointmentCommandHandler>();
			services.AddScoped<IRequestHandler<DenyAppointmentCommand, ValidationResult>, AppointmentCommandHandler>();
			services.AddScoped<IRequestHandler<InProcessAppointmentCommand, ValidationResult>, AppointmentCommandHandler>();
			services.AddScoped<IRequestHandler<UpdateAppointmentCommand, ValidationResult>, AppointmentCommandHandler>();

			// Domain - Queries
			services.AddScoped<IRequestHandler<GetAllAppointmentsByUserId, IEnumerable<AppointmentModel>>, AppointmentQueryHandler>();
			services.AddScoped<IRequestHandler<GetAppointmentByIdAndUserId, AppointmentModel>, AppointmentQueryHandler>();


		}
	}
}
