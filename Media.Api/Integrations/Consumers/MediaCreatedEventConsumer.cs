using Events.Media;
using MassTransit;
using MediatR;

namespace Media.Api.Integrations.Consumers
{
	public class MediaCreatedEventConsumer : IConsumer<MediaCreatedEvent>
	{
		private readonly ILogger<MediaCreatedEventConsumer> _logger;
		private readonly IMediator _mediator;

		public MediaCreatedEventConsumer(ILogger<MediaCreatedEventConsumer> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}

		public async Task Consume(ConsumeContext<MediaCreatedEvent> context)
		{

			try
			{
				await _mediator.Publish(context.Message);

				await context.RespondAsync(new MediaSuccessfulEvent((Guid)context.CorrelationId, context.Message.Id, context.Message.ImageOwnerId));

			}
			catch (Exception ex)
			{
				await context.RespondAsync(new MediaRejectedEvent((Guid)context.CorrelationId, context.Message.Id, context.Message.ImageOwnerId, ex.Message));

			}
		}
	}
}
