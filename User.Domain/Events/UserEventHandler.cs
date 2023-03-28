using AutoMapper;
using Events.User;
using MediatR;
using Microsoft.Extensions.Logging;
using User.Domain.Commands.User;

namespace User.Domain.Events
{
	public class UserEventHandler : INotificationHandler<UserCreatedEvent>,
									INotificationHandler<UserDeletedEvent>,
									INotificationHandler<UserUpdatedEvent>
	{
		private readonly ILogger<UserEventHandler> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public UserEventHandler(ILogger<UserEventHandler> logger, IMediator mediator, IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<CreateUserCommand>(notification));
			_logger.LogInformation($"user created :{notification.Id}");
		}

		public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
		{
			var commnand = _mapper.Map<DeleteUserCommand>(notification);
			await _mediator.Send(commnand);
			_logger.LogInformation($"user deleted :{notification.Id}");
		}

		public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<UpdateUserCommand>(notification));
			_logger.LogInformation($"user updated :{notification.Id}");
		}
	}
}
