using Notification.Api.Dto;

namespace Notification.Api.Hubs
{
	public class UserTracker
	{
		public readonly ICollection<UserConnectionInfoDto> OnlineUsers = new List<UserConnectionInfoDto>();
		public Task<bool> UserConnected(UserConnectionInfoDto user, string connectionId)
		{
			bool isOnline = false;
			lock (OnlineUsers)
			{
				var temp = OnlineUsers.FirstOrDefault(x => x.UserId == user.UserId);

				if (temp == null)
				{
					user.SetConnectionId(connectionId);
					OnlineUsers.Add(user);
					isOnline = true;
				}
			}

			return Task.FromResult(isOnline);
		}

		public void UserDisconnected(Guid userId)
		{
			lock (OnlineUsers)
			{
				var user = OnlineUsers.FirstOrDefault(x => x.UserId == userId);

				OnlineUsers.Remove(user);
			}

		}

		public IEnumerable<string>? GetConnectionIds(Guid userId)
		{
			lock (OnlineUsers)
			{
				var connectionIds = OnlineUsers.Where(x => x.UserId == userId)?.Select(x=>x.ConnectionId).ToList();

				return connectionIds;

			}

		}

		public Task<bool> IsUserOnline(Guid userId)
		{
			lock(OnlineUsers)
			{
				var isOnline = OnlineUsers.Any(x => x.UserId == userId);

				return Task.FromResult(isOnline);
			}
		}

	}
}
