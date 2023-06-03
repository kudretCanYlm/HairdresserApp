using Events.HairdresserService;
using Events.MassTransitOptions;
using Events.Media;
using MassTransit;

namespace HairdresserService.Domain.Sagas.HairdresserServiceMedia
{
	public class HairdresserServiceMediaStateMachine : MassTransitStateMachine<HairdresserServiceMediaStateInstance>
	{
		//Create
		public Event<HairdresserServiceMediaCreatedEvent> HairdresserServiceMediaCreatedEvent { get; set; }
		public Event<MediaCreatedRejectedEvent> HairdresserServiceMediaCreatedRejectedEvent { get; set; }
		public Event<MediaCreatedSuccessfulEvent> HairdresserServiceMediaCreatedSuccessfulEvent { get; set; }

		//Delete
		public Event<HairdresserServiceMediaDeletedEvent> HairdresserServiceMediaDeletedEvent { get; set; }
		public Event<MediaDeletedRejectedEvent> HairdresserServiceMediaDeletedRejectedEvent { get; set; }
		public Event<MediaDeletedSuccessfulEvent> HairdresserServiceMediaDeletedSuccessfulEvent { get; set; }

		//Update
		public Event<HairdresserServiceMediaUpdatedEvent> HairdresserServiceMediaUpdatedEvent { get; set; }
		public Event<MediaUpdatedRejectedEvent> HairdresserServiceMediaUpdatedRejectedEvent { get; set; }
		public Event<MediaUpdatedSuccessfulEvent> HairdresserServiceMediaUpdatedSuccessfulEvent { get; set; }



		//Create
		public State HairdresserServiceMediaCreated { get; set; }
		public State HairdresserServiceMediaCreatedRejected { get; set; }
		public State HairdresserServiceMediaCreatedSuccessful { get; set; }

		//Update
		public State HairdresserServiceMediaDeleted { get; set; }
		public State HairdresserServiceMediaDeletedRejected { get; set; }
		public State HairdresserServiceMediaDeletedSuccessful { get; set; }


		//Delete
		public State HairdresserServiceMediaUpdated { get; set; }
		public State HairdresserServiceMediaUpdatedRejected { get; set; }
		public State HairdresserServiceMediaUpdatedSuccessful { get; set; }


		public HairdresserServiceMediaStateMachine()
		{
			InstanceState(ins => ins.CurrentState);

			//CreateMedia
			Event(() => HairdresserServiceMediaCreatedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateBy<Guid>(database => database.CorrelationId, @event => @event.Message.Id)
				.SelectId(e => Guid.NewGuid()));

			Event(() => HairdresserServiceMediaCreatedRejectedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Event(() => HairdresserServiceMediaCreatedSuccessfulEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));


			//Update
			Event(() => HairdresserServiceMediaUpdatedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateBy<Guid>(database => database.CorrelationId, @event => @event.Message.Id)
				.SelectId(e => Guid.NewGuid()));

			Event(() => HairdresserServiceMediaUpdatedRejectedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Event(() => HairdresserServiceMediaUpdatedSuccessfulEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			//Delete
			Event(() => HairdresserServiceMediaDeletedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateBy<Guid>(database => database.CorrelationId, @event => @event.Message.Id)
				.SelectId(e => Guid.NewGuid()));

			Event(() => HairdresserServiceMediaDeletedRejectedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Event(() => HairdresserServiceMediaDeletedSuccessfulEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			//Create
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
				));

			During(HairdresserServiceMediaCreated,

				When(HairdresserServiceMediaCreatedRejectedEvent)
				.Then(x => Console.WriteLine("başarısız resim oluşturma işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaCreatedRejected)
				.Finalize(),

				When(HairdresserServiceMediaCreatedSuccessfulEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.OwnerId;
				})
				.Then(x => Console.WriteLine("başarılı resim oluşturma işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaCreatedSuccessful)
				.Finalize()
				);

			//Update
			Initially(
				When(HairdresserServiceMediaUpdatedEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.ImageOwnerId;
					context.Instance.ImageId = context.Data.Id;
				})
				.TransitionTo(HairdresserServiceMediaUpdated)
				.Send(new Uri($"queue:{RabbitMqQueues.Media_MediaUpdatedEventQueue}"),
					context => new MediaUpdatedEvent(context.Instance.CorrelationId, context.Instance.ImageId, context.Data.FileExtension, context.Data.MediaData, context.Data.CustomType, context.Data.ImageOwnerId)
				));

			During(HairdresserServiceMediaUpdated,

				When(HairdresserServiceMediaUpdatedRejectedEvent)
				.Then(x => Console.WriteLine("başarısız resim güncelleme işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaUpdatedRejected)
				.Finalize(),

				When(HairdresserServiceMediaUpdatedSuccessfulEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.OwnerId;
				})
				.Then(x => Console.WriteLine("başarılı resim güncelleme işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaUpdatedSuccessful)
				.Finalize()
				);

			//Delete
			Initially(
				When(HairdresserServiceMediaDeletedEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.ImageOwnerId;
					context.Instance.ImageId = context.Data.Id;
				})
				.TransitionTo(HairdresserServiceMediaDeleted)
				.Send(new Uri($"queue:{RabbitMqQueues.Media_MediaDeletedEventQueue}"),
					context => new MediaDeletedEvent(context.Instance.CorrelationId, context.Instance.ImageId,context.Data.ImageOwnerId)
				));

			During(HairdresserServiceMediaDeleted,

				When(HairdresserServiceMediaDeletedRejectedEvent)
				.Then(x => Console.WriteLine("başarısız resim silme işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaDeletedRejected)
				.Finalize(),

				When(HairdresserServiceMediaDeletedSuccessfulEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.OwnerId;
				})
				.Then(x => Console.WriteLine("başarılı resim silme işlemi :" + x.Message.Id))
				.TransitionTo(HairdresserServiceMediaDeletedSuccessful)
				.Finalize()
				);


			SetCompletedWhenFinalized();
		}
	}
}
