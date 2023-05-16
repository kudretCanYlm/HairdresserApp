using AutoMapper;
using Events.Hairdresser;
using Hairdresser.Domain.Commands.Hairdresser;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hairdresser.Domain.Events
{
	public class HairdresserEventHandler : INotificationHandler<HairdresserCreatedEvent>,
										INotificationHandler<HairdresserDeletedEvent>,
										INotificationHandler<HairdresserUpdatedEvent>
	{
		private readonly ILogger<HairdresserEventHandler> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public HairdresserEventHandler(ILogger<HairdresserEventHandler> logger, IMediator mediator, IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task Handle(HairdresserCreatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<CreateHairdresserCommand>(notification));
			_logger.LogInformation($"Hairdresser created :{notification.Id}");
		}

		public async Task Handle(HairdresserDeletedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<DeleteHairdresserCommand>(notification));
			_logger.LogInformation($"Hairdresser deleted :{notification.Id}");
		}

		public async Task Handle(HairdresserUpdatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<UpdateHairdresserCommand>(notification));
			_logger.LogInformation($"Hairdresser updated :{notification.Id}");
		}
	}
}
