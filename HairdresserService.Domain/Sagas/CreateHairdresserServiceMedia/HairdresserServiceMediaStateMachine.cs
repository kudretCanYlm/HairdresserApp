using Events.HairdresserService;
using Events.MassTransitOptions;
using Events.Media;
using Events.User;
using MassTransit;

namespace HairdresserService.Domain.Sagas.CreateHairdresserServiceMedia
{
	public class HairdresserServiceMediaStateMachine : MassTransitStateMachine<HairdresserServiceMediaStateInstance>
	{
		public Event<HairdresserServiceMediaCreatedEvent> HairdresserServiceMediaCreatedEvent { get; set; }
		public Event<MediaRejectedEvent> HairdresserServiceMediaRejectedEvent { get; set; }
		public Event<MediaSuccessfulEvent> HairdresserServiceMediaSuccessfulEvent { get; set; }

		public State HairdresserServiceMediaCreated { get; set; }
		public State HairdresserServiceMediaRejected { get; set; }
		public State HairdresserServiceMediaSuccessful { get; set; }

		public HairdresserServiceMediaStateMachine()
		{
			InstanceState(ins => ins.CurrentState);

			Event(() => HairdresserServiceMediaCreatedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateBy<Guid>(database => database.CorrelationId, @event => @event.Message.Id)
				.SelectId(e => Guid.NewGuid()));


			Event(() => HairdresserServiceMediaRejectedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Event(() => HairdresserServiceMediaSuccessfulEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Initially(
				When(HairdresserServiceMediaCreatedEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.ImageOwnerId;
					context.Instance.ImageId = context.Data.Id;
				})
				.TransitionTo(HairdresserServiceMediaCreated)
				.Send(new Uri($"queue:{RabbitMqQueues.Media_MediaCreatedEventQueue}"),
					context => new MediaCreatedEvent(context.Instance.CorrelationId, context.Instance.ImageId, context.Data.FileExtension, context.Data.MediaData, context.Data.CustomType, context.Data.ImageOwnerId)
				)); ;

			During(HairdresserServiceMediaCreated,

				When(HairdresserServiceMediaRejectedEvent)
				//.Then(context =>
				//{
				//	context.Instance.Version = 1;
				//})
				.Then(x => Console.WriteLine("başarısız resim işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaRejected)
				.Finalize(),

				When(HairdresserServiceMediaSuccessfulEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.OwnerId;
				})
				.Then(x => Console.WriteLine("başarılı resim işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaSuccessful)
				.Finalize()
				);

			SetCompletedWhenFinalized();
		}
	}
}
