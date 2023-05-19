using Database.Entity;
using Events.Appointment.Enum;
using NetDevPack.Domain;

namespace Appointment.Domain.Models
{
	public class AppointmentModel:BaseEntity,IAggregateRoot
	{
		public AppointmentModel()
		{
			Id= Guid.NewGuid();
		}

		public AppointmentStateEnum AppointmentState { get; set; }
		public string Notes { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public TimeSpan AppointmentEndTime { get; set; }
		public Guid UserId { get; set; }
		public Guid HairdresserServiceId { get; set; }
		public Guid HairdresserId { get; set; }

	}
}
