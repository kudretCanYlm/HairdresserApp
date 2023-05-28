using MassTransit;

namespace Hairdresser.Domain.Sagas.CreateHairdresserMedia
{
	public class HairdresserMediaStateInstance: SagaStateMachineInstance, ISagaVersion
	{
		public HairdresserMediaStateInstance(Guid correlationId)
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
