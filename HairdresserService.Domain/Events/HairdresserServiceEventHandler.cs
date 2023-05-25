using AutoMapper;
using Events.HairdresserService;
using HairdresserService.Domain.Commands.HairdresserService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HairdresserService.Domain.Events
{
	public class HairdresserServiceEventHandler : INotificationHandler<HairdresserServiceActivatedEvent>,
												INotificationHandler<HairdresserServiceCreatedEvent>,
												INotificationHandler<HairdresserServiceDeactivatedEvent>,
												INotificationHandler<HairdresserServiceDeletedEvent>,
												INotificationHandler<HairdresserServiceUpdatedEvent>
	{
		private readonly ILogger<HairdresserServiceEventHandler> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public HairdresserServiceEventHandler(ILogger<HairdresserServiceEventHandler> logger, IMediator mediator, IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task Handle(HairdresserServiceActivatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<ActivateHairdresserServiceCommand>(notification));
		}

		public async Task Handle(HairdresserServiceCreatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<CreateHairdresserServiceCommand>(notification));
		}

		public async Task Handle(HairdresserServiceDeletedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<DeleteHairdresserServiceCommand>(notification));
		}

		public async Task Handle(HairdresserServiceDeactivatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<DeactivateHairdresserServiceCommand>(notification));
		}

		public async Task Handle(HairdresserServiceUpdatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<UpdateHairdresserServiceCommand>(notification));
		}
	}
}
