using AutoMapper;
using Events.User.Address;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Commands.Address;
using User.Domain.Commands.User;

namespace User.Domain.Events
{
	public class AddressEventHandler : INotificationHandler<UserAddressCreatedEvent>,
									  INotificationHandler<UserAddressDeletedEvent>,
									  INotificationHandler<UserAddressUpdatedEvent>
	{
		private readonly ILogger<UserEventHandler> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public AddressEventHandler(ILogger<UserEventHandler> logger, IMediator mediator, IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task Handle(UserAddressCreatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<CreateUserAddressCommand>(notification));
			_logger.LogInformation($"user address created :{notification.Id}");
		}

		public async Task Handle(UserAddressDeletedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<DeleteUserAddressCommand>(notification));
			_logger.LogInformation($"user address deleted :{notification.Id}");
		}

		public async Task Handle(UserAddressUpdatedEvent notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(_mapper.Map<UpdateUserAddressCommand>(notification));
			_logger.LogInformation($"user address updated :{notification.Id}");
		}
	}
}
