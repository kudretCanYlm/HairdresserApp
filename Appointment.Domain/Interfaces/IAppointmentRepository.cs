using Appointment.Domain.Models;
using Database.Repository;

namespace Appointment.Domain.Interfaces
{
	public interface IAppointmentRepository:IBaseRepository<AppointmentModel>
	{
		Task<AppointmentModel> GetAppointmentByIdAndUserId(Guid id, Guid userId);
		Task<AppointmentModel> GetAppointmentByIdAndHairdresserId(Guid id, Guid hairdresserId);
		Task<IEnumerable<AppointmentModel>> GetAllAppointmentsByUserId(Guid userId);
	}
}
