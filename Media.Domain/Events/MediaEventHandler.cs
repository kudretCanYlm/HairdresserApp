using AutoMapper;
using Events.Media;
using Media.Domain.Commands.Media;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain.Events
{
	public class MediaEventHandler : INotificationHandler<MediaCreatedEvent>,
									INotificationHandler<MediaDeletedEvent>,
									INotificationHandler<MediaUpdatedEvent>
	{
		private readonly ILogger<MediaEventHandler> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public MediaEventHandler(ILogger<MediaEventHandler> logger, IMediator mediator, IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task Handle(MediaCreatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<CreateMediaCommand>(notification));
			_logger.LogInformation($"media created :{notification.Id}");
		}

		public async Task Handle(MediaDeletedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<DeleteMediaCommand>(notification));
			_logger.LogInformation($"media deleted :{notification.Id}");
		}

		public async Task Handle(MediaUpdatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<UpdateMediaCommand>(notification));
			_logger.LogInformation($"media updated :{notification.Id}");
		}
	}
}
