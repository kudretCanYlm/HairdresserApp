namespace Appointment.Application.Dto
{
	public class GetAllAppointmentsForUserPostDto
	{
		public Guid HairdresserId { get; set; }
		public DateTime AppointmentDate { get; set; }
	}
}
