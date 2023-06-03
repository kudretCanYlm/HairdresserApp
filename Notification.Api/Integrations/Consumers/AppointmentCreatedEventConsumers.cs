using Events.Appointment;
using Events.Appointment.Enum;
using Grpc.Hairdresser.ClientServices;
using Grpc.HairdresserService.ClientServices;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Notification.Api.Hubs;
using Notification.Api.Interfaces;
using Notification.Api.Models;

namespace Notification.Api.Integrations.Consumers
{
	public class AppointmentCreatedEventConsumers : IConsumer<AppointmentCreatedEvent>
	{
		private readonly IHubContext<NotificationHub> _connectionHub;
		private readonly HairdresserGrpcService _hairdresserGrpcService;
		private readonly HairdresserServiceGrpcService _hairdresserServiceGrpcService;
		private readonly UserTracker _userTracker;
		private readonly INotificationRepository _notificationRepository;

		public AppointmentCreatedEventConsumers(IHubContext<NotificationHub> connectionHub, HairdresserGrpcService hairdresserGrpcService, HairdresserServiceGrpcService hairdresserServiceGrpcService, UserTracker userTracker, INotificationRepository notificationRepository)
		{
			_connectionHub = connectionHub;
			_hairdresserGrpcService = hairdresserGrpcService;
			_hairdresserServiceGrpcService = hairdresserServiceGrpcService;
			_userTracker = userTracker;
			_notificationRepository = notificationRepository;
		}

		public async Task Consume(ConsumeContext<AppointmentCreatedEvent> context)
		{
			var appointmentNotification = new AppointmentModel
			{
				SendTo = (await _hairdresserGrpcService.GetHairdresserOwnerId(context.Message.HairdresserId)).ToString("N"),
				Message = $"Yeni randevu bildirimi:\n" +
				$"Randevu Tarihi:${context.Message.AppointmentDate}\n" +
				$"Randevu Başlangıç Saati:${context.Message.AppointmentStartTime}\n" +
				$"Randevu Bitiş Saati:${context.Message.AppointmentEndTime}\n" +
				$"Kuafor Hizmeti:${await _hairdresserServiceGrpcService.GetHairdresserServiceNameById(context.Message.HairdresserServiceId, context.Message.HairdresserId)} \n" +
				$"Randevu Durumu:${Enum.GetName(typeof(AppointmentStateEnum), context.Message.AppointmentState)}",
				AppointmentId = context.Message.Id,
				Id = Guid.NewGuid(),
			};

			bool isUserOnline = await _userTracker.IsUserOnline(Guid.Parse(appointmentNotification.SendTo));


			if (isUserOnline)
			{
				var connId = await _userTracker.GetConnectionId(Guid.Parse(appointmentNotification.SendTo));

				await _connectionHub
					.Clients.Client(connId)
					.SendAsync("AppointmentNotification", appointmentNotification);
			}
			else
				await _notificationRepository.AddAppointmentNotification(appointmentNotification);
			


		}
	}
}
