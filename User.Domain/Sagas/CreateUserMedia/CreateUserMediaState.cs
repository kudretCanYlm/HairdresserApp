using Automatonymous;
using MassTransit;
using MongoDB.Bson.Serialization.Attributes;

namespace User.Domain.Sagas.CreateUserMedia
{
	public class CreateUserMediaState : SagaStateMachineInstance, ISagaVersion
	{
		public CreateUserMediaState(Guid correlationId)
		{
			CorrelationId = correlationId;
		}
		
		public Guid CorrelationId { get; set; }
		
		public Guid ImageId { get; set; }
		public Guid ImageOwnerId { get; set; }
		
		public string CurrentState { get; set; }
		public int Version { get ; set ; }
	}
}
