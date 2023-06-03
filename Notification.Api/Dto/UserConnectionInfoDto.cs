namespace Notification.Api.Dto
{
	public class UserConnectionInfoDto
	{
		public UserConnectionInfoDto(Guid userId)
		{
			UserId = userId;
			ConnectionTime= DateTime.UtcNow;
		}

		public Guid UserId { get; set; }
		public DateTime ConnectionTime { get;private set; }
		public string ConnectionId { get;private set; }

		public void SetConnectionId(string connectionId)
		{
			ConnectionId= connectionId;
		}
	}
}
