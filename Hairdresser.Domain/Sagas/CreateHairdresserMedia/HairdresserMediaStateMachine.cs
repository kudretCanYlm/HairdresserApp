using Events.Hairdresser;
using Events.MassTransitOptions;
using Events.Media;
using MassTransit;

namespace Hairdresser.Domain.Sagas.CreateHairdresserMedia
{
    public class HairdresserMediaStateMachine : MassTransitStateMachine<HairdresserMediaStateInstance>
	{
		public Event<HairdresserMediaCreatedEvent> HairdresserMediaCreatedEvent { get; set; }
		public Event<MediaRejectedEvent> HairdresserMediaRejectedEvent { get; set; }
		public Event<MediaSuccessfulEvent> HairdresserMediaSuccessfulEvent { get; set; }

		public State HairdresserMediaCreated { get; set; }
		public State HairdresserMediaRejected { get; set; }
		public State HairdresserMediaSuccessful { get; set; }

		public HairdresserMediaStateMachine()
		{
			InstanceState(ins => ins.CurrentState);

			Event(() => HairdresserMediaCreatedEvent,
				stateInstance =>
				stateInstance.CorrelateBy<Guid>(database => database.CorrelationId, @event => @event.Message.Id)
				.SelectId(e => Guid.NewGuid()));

			Event(() => HairdresserMediaRejectedEvent,
				stateInstance =>
				stateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Event(() => HairdresserMediaSuccessfulEvent,
				stateInstance =>
				stateInstance.CorrelateById(@event => @event.Message.CorrelationId));


			Initially(
					When(HairdresserMediaCreatedEvent)
					.Then(context =>
					{
						context.Instance.ImageOwnerId = context.Data.ImageOwnerId;
						context.Instance.ImageId = context.Data.Id;
					})
					.TransitionTo(HairdresserMediaCreated)
					.Send(new Uri($"queue:{RabbitMqQueues.Media_MediaCreatedEventQueue}"),
					context => new MediaCreatedEvent(context.Instance.CorrelationId, context.Instance.ImageId, context.Data.FileExtension, context.Data.MediaData, context.Data.CustomType, context.Data.ImageOwnerId)
					));

			During(HairdresserMediaCreated,

				When(HairdresserMediaRejectedEvent)
				.Then(x => Console.WriteLine("başarısız resim işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserMediaRejected)
				.Finalize(),

				When(HairdresserMediaSuccessfulEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.OwnerId;
				})
				.Then(x => Console.WriteLine("başarılı resim işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserMediaSuccessful)
				.Finalize()
				);

			SetCompletedWhenFinalized();
		}


	}
}
