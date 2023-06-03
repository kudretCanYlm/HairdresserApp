using Grpc.Auth.ClientServices;
using Microsoft.AspNetCore.SignalR;
using NetDevPack.Identity.User;
using Notification.Api.Dto;
using Notification.Api.Interfaces;

namespace Notification.Api.Hubs
{
	public class NotificationHub : Hub
	{
		private readonly UserTracker _userTracker;
		private readonly AuthGrpcService _authGrpcService;
		private readonly INotificationRepository _notificationRepository;

		public NotificationHub(UserTracker userTracker, AuthGrpcService authGrpcService, INotificationRepository notificationRepository)
		{
			_userTracker = userTracker;
			_authGrpcService = authGrpcService;
			_notificationRepository = notificationRepository;
		}

		public override async Task OnConnectedAsync()
		{
			var token = Context.GetHttpContext().Request.Query["access_token"].FirstOrDefault();

			if (token == null)
				return;

			var user = await _authGrpcService.CheckTokenRefreshAndUserId((string)token);
			var userId = Guid.Parse(user.UserId);

			await _userTracker.UserConnected(new UserConnectionInfoDto(userId), Context.ConnectionId);
			var sdfsd = _userTracker.OnlineUsers;
			var connId = await _userTracker.GetConnectionId(Guid.Parse(user.UserId));


			var userIdString = userId.ToString("N");
			var messages = await _notificationRepository.GetAppointmentNotifications(userIdString);

			foreach (var message in messages)
			{
				await Clients.Client(connId).SendAsync("AppointmentNotification", message);
				await _notificationRepository.DeleteAppointmentNotifications(message);
			}

		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var token = Context.GetHttpContext().Request.Query["access_token"].FirstOrDefault();

			if (token == null)
				return;

			var user = await _authGrpcService.CheckTokenRefreshAndUserId((string)token);
			var userId = Guid.Parse(user.UserId);

			_userTracker.UserDisconnected(userId);
		}
	}
}
