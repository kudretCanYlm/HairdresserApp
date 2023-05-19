using Appointment.Domain.Commands.Appointment;
using AutoMapper;
using Events.Appointment;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;

namespace Appointment.Domain.Events
{
	//add logs later
	public class AppointmentEventHandler : INotificationHandler<AppointmentApprovedEvent>,
											INotificationHandler<AppointmentCanceledEvent>,
											INotificationHandler<AppointmentCompletedEvent>,
											INotificationHandler<AppointmentCreatedEvent>,
											INotificationHandler<AppointmentDeniedEvent>,
											INotificationHandler<AppointmentInProcessedEvent>,
											INotificationHandler<AppointmentUpdatedEvent>
	{

		private readonly ILogger<AppointmentEventHandler> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public AppointmentEventHandler(ILogger<AppointmentEventHandler> logger, IMediator mediator, IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task Handle(AppointmentApprovedEvent notification, CancellationToken cancellationToken)
		{
			await SendAppointmentEvent<ApproveAppointmentCommand>(notification);
		}

		public async Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
		{
			await SendAppointmentEvent<CancelAppointmentCommand>(notification);

		}

		public async Task Handle(AppointmentCompletedEvent notification, CancellationToken cancellationToken)
		{
			await SendAppointmentEvent<CompleteAppointmentCommand>(notification);

		}

		public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
		{
			await SendAppointmentEvent<CreateAppointmentCommand>(notification);

		}

		public async Task Handle(AppointmentDeniedEvent notification, CancellationToken cancellationToken)
		{
			await SendAppointmentEvent<DenyAppointmentCommand>(notification);

		}

		public async Task Handle(AppointmentInProcessedEvent notification, CancellationToken cancellationToken)
		{
			await SendAppointmentEvent<InProcessAppointmentCommand>(notification);
		}

		public async Task Handle(AppointmentUpdatedEvent notification, CancellationToken cancellationToken)
		{
			 await SendAppointmentEvent<UpdateAppointmentCommand>(notification);
		}

		private async Task SendAppointmentEvent<T>(Event notification) where T:AppointmentCommand
		{
			await _mediator.Send(_mapper.Map<T>(notification));
		}
	}
}
