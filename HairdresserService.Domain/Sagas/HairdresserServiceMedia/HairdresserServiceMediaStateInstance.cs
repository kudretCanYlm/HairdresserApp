using MassTransit;

namespace HairdresserService.Domain.Sagas.HairdresserServiceMedia
{
	public class HairdresserServiceMediaStateInstance: SagaStateMachineInstance, ISagaVersion
	{
		public HairdresserServiceMediaStateInstance(Guid correlationId)
		{
			CorrelationId = correlationId;
		}

		public Guid CorrelationId { get; set; }

		public Guid ImageId { get; set; }
		public Guid ImageOwnerId { get; set; }

		public string CurrentState { get; set; }
		public int Version { get; set; }
	}
}
