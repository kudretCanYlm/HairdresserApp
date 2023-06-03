using Events.Media;
using MassTransit;
using MediatR;

namespace Media.Api.Integrations.Consumers
{
	public class MediaUpdatedEventConsumer : IConsumer<MediaUpdatedEvent>
	{
		private readonly IMediator _mediator;

		public MediaUpdatedEventConsumer(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task Consume(ConsumeContext<MediaUpdatedEvent> context)
		{
			try
			{
				await _mediator.Publish(context.Message);
				await context.RespondAsync(new MediaUpdatedSuccessfulEvent((Guid)context.CorrelationId, context.Message.Id, context.Message.ImageOwnerId));

			}
			catch (Exception ex)
			{
				await context.RespondAsync(new MediaUpdatedRejectedEvent((Guid)context.CorrelationId, context.Message.Id, context.Message.ImageOwnerId, ex.Message));
			}
		}
	}
}
