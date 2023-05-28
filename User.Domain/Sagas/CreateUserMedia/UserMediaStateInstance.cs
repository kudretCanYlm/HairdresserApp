using MassTransit;

namespace User.Domain.Sagas.CreateUserMedia
{
	public class UserMediaStateInstance : SagaStateMachineInstance, ISagaVersion
	{
		public UserMediaStateInstance(Guid correlationId)
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
