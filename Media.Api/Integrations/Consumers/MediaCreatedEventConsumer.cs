using Events.MassTransitOptions;
using Events.Media;
using Events.User;
using MassTransit;
using MediatR;
using NetDevPack.Mediator;

namespace Media.Api.Integrations.Consumers
{
	public class MediaCreatedEventConsumer : IConsumer<MediaCreatedEvent>
	{
		private readonly ILogger<MediaCreatedEventConsumer> _logger;
		private readonly ISendEndpointProvider _sendEndpointProvider;
		private readonly IMediator _mediator;

		public MediaCreatedEventConsumer(ILogger<MediaCreatedEventConsumer> logger, ISendEndpointProvider sendEndpointProvider, IMediator mediator)
		{
			_logger = logger;
			_sendEndpointProvider = sendEndpointProvider;
			_mediator = mediator;
		}

		public async Task Consume(ConsumeContext<MediaCreatedEvent> context)
		{
			ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueues.StateMachine_UserMedia}"));

			try
			{
				await _mediator.Publish(context.Message);

				await sendEndpoint.Send(new UserMediaSuccessfulEvent((Guid)context.CorrelationId, context.Message.Id,context.Message.ImageOwnerId));

			}
			catch (Exception ex)
			{
				await sendEndpoint.Send(new UserMediaRejectedEvent(context.Message.CorrelationId, context.Message.Id,context.Message.ImageOwnerId, ex.Message));
			}
		}
	}
}
