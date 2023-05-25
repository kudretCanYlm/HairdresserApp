using Events.MassTransitOptions;
using Events.Media;
using Events.User;
using MassTransit;

namespace User.Domain.Sagas.CreateUserMedia
{
	public class CreateUserMediaStateMachine : MassTransitStateMachine<CreateUserMediaState>
	{
		//https://github.com/gncyyldz/Saga-Orchestration/tree/master/Order.API
		//https://www.gencayyildiz.com/blog/microservice-saga-commands-orchestration-implemantasyonu-ile-transaction-yonetimi/	

		public Event<UserMediaCreatedEvent> UserMediaCreatedEvent { get; set; }
		public Event<UserMediaRejectedEvent> UserMediaRejectedEvent { get; set; }
		public Event<UserMediaSuccessfulEvent> UserMediaSuccessfulEvent { get; set; }

		public State UserMediaCreated { get; set; }
		public State UserMediaRejected { get; set; }
		public State UserMediaSuccessful { get; set; }

		public CreateUserMediaStateMachine()
		{
			//State Instance'da ki hangi property'nin sipariş sürecindeki state'i tutacağı bildiriliyor.
			//Yani artık tüm event'ler CurrentState property'sin de tutulacaktır!
			InstanceState(ins => ins.CurrentState);

			//Eğer gelen event OrderStartedEvent ise CorrelateBy metodu ile veritabanında(database)
			//tutulan Order State Instance'da ki OrderId'si ile gelen event'te ki(@event) OrderId'yi
			//kıyasla. Bu kıyas neticesinde eğer ilgili instance varsa kaydetme. Yani yeni bir korelasyon
			//üretme! Yok eğer yoksa yeni bir korelasyon üret(SelectId)
			Event(() => UserMediaCreatedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateBy<Guid>(database => database.CorrelationId, @event => @event.Message.Id)
				.SelectId(e => Guid.NewGuid()));

			//StockReservedEvent fırlatıldığında veritabanındaki hangi correlationid değerine sahip state
			//instance'ın state'ini değiştirecek bunu belirtmiş olduk!
			Event(() => UserMediaRejectedEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Event(() => UserMediaSuccessfulEvent,
				orderStateInstance =>
				orderStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

			Initially(
				When(UserMediaCreatedEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.ImageOwnerId;
					context.Instance.ImageId = context.Data.Id;
				})
				.TransitionTo(UserMediaCreated)
				.Send(new Uri($"queue:{RabbitMqQueues.Media_MediaCreatedEventQueue}"),
				context => new MediaCreatedEvent(context.Instance.CorrelationId, context.Instance.ImageId, context.Data.FileExtension,context.Data.MediaData,context.Data.CustomType,context.Data.ImageOwnerId)
				)); ;

			During(UserMediaCreated,

				When(UserMediaRejectedEvent)
				//.Then(context =>
				//{
				//	context.Instance.Version = 1;
				//})
				.TransitionTo(UserMediaRejected)
				.Finalize(),

				When(UserMediaSuccessfulEvent)
				.Then(context =>
				{
					context.Instance.ImageOwnerId = context.Data.OwnerId;
				})
				.TransitionTo(UserMediaSuccessful)
				.Finalize()
				);



			////DuringAny(When(CreateOrderFaultEvent)
			////.TransitionTo(CreateOrderFaultedState)
			////.Then(context => context.Publish<Fault<TakePaymentEvent>>(new { context.Message })));

			SetCompletedWhenFinalized();
		}

		// public Event<Fault<CreateOrderEvent>> CreateOrderFaultEvent { get; }



	}
}
