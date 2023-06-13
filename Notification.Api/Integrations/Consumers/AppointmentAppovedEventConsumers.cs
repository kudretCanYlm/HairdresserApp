using Events.Appointment;
using Events.Appointment.Enum;
using Grpc.HairdresserService.ClientServices;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Notification.Api.Hubs;
using Notification.Api.Interfaces;
using Notification.Api.Models;

namespace Notification.Api.Integrations.Consumers
{
	public class AppointmentAppovedEventConsumers : IConsumer<AppointmentApprovedEvent>
	{
		private readonly IHubContext<NotificationHub> _connectionHub;
		private readonly HairdresserServiceGrpcService _hairdresserServiceGrpcService;
		private readonly UserTracker _userTracker;
		private readonly INotificationRepository _notificationRepository;

		public AppointmentAppovedEventConsumers(IHubContext<NotificationHub> connectionHub, HairdresserServiceGrpcService hairdresserServiceGrpcService, UserTracker userTracker, INotificationRepository notificationRepository)
		{
			_connectionHub = connectionHub;
			_hairdresserServiceGrpcService = hairdresserServiceGrpcService;
			_userTracker = userTracker;
			_notificationRepository = notificationRepository;
		}

		public async Task Consume(ConsumeContext<AppointmentApprovedEvent> context)
		{

				var appointmentNotification = new AppointmentModel
				{
					SendTo = context.Message.UserId.ToString("N"),
					Message = $"Yeni randevu hareketi bildirimi:\n" +
					$"Kuafor Hizmeti:${await _hairdresserServiceGrpcService.GetHairdresserServiceNameById(context.Message.HairdresserServiceId, context.Message.HairdresserId)} \n" +
					$"Randevu Durumu:${Enum.GetName(typeof(AppointmentStateEnum), context.Message.AppointmentState)}",
					AppointmentId = context.Message.Id,
					Id = Guid.NewGuid()
				};

				bool isUserOnline = await _userTracker.IsUserOnline(Guid.Parse(appointmentNotification.SendTo));

				if (isUserOnline)
				{
					var connIds = _userTracker.GetConnectionIds(Guid.Parse(appointmentNotification.SendTo));

					await _connectionHub.Clients.Clients(connIds).SendAsync("AppointmentNotification", appointmentNotification);
				}
				else
					await _notificationRepository.AddAppointmentNotification(appointmentNotification);

		}
	}
}
