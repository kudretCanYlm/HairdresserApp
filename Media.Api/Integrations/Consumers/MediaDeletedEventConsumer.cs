using Events.Media;
using MassTransit;
using MediatR;

namespace Media.Api.Integrations.Consumers
{
	public class MediaDeletedEventConsumer : IConsumer<MediaDeletedEvent>
	{
		private readonly IMediator _mediator;

		public MediaDeletedEventConsumer(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task Consume(ConsumeContext<MediaDeletedEvent> context)
		{
			try
			{
				await _mediator.Publish(context.Message);
				await context.RespondAsync(new MediaDeletedSuccessfulEvent((Guid)context.CorrelationId, context.Message.Id, context.Message.ImageOwnerId));

			}
			catch (Exception ex)
			{
				await context.RespondAsync(new MediaDeletedRejectedEvent((Guid)context.CorrelationId, context.Message.Id, context.Message.ImageOwnerId, ex.Message));
			}
		}
	}
}
