using Notification.Api.Models;

namespace Notification.Api.Interfaces
{
	public interface INotificationRepository
	{
		Task<IEnumerable<AppointmentModel>> GetAppointmentNotifications(string userId);
		Task DeleteAppointmentNotifications(AppointmentModel appointmentModel);
		Task AddAppointmentNotification(AppointmentModel appointmentModel);
	}
}
