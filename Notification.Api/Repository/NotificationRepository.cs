using Database.Repository.Redis;
using Notification.Api.Interfaces;
using Notification.Api.Models;

namespace Notification.Api.Repository
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly IRedisBaseRepository<AppointmentModel> _redisBaseRepository;

		public NotificationRepository(IRedisBaseRepository<AppointmentModel> redisBaseRepository)
		{
			_redisBaseRepository = redisBaseRepository;
		}

		public async Task AddAppointmentNotification(AppointmentModel appointmentModel)
		{
			await _redisBaseRepository.InsertAsync(appointmentModel);
		}

		public async Task DeleteAppointmentNotifications(AppointmentModel appointmentModel)
		{
			await _redisBaseRepository.DeleteAsync(appointmentModel);
		}

		public async Task<IEnumerable<AppointmentModel>> GetAppointmentNotifications(string userId)
		{
			try
			{
				var notifications = await _redisBaseRepository.GetAllByWhereAsync(x => x.SendTo == userId);

				//if (notifications.Count()>0)
					//await _redisBaseRepository.DeleteMulti(x => x.SendTo == userId);

				return notifications;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}
	}
}
