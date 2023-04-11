namespace Auth.Application.Dto.Auth
{
	public class UserAuthSessionDto
	{
		public Guid Id { get; set; }
		public string Token { get; set; }
		public Guid TokenOwnerId { get; set; }
		public DateTime TokenExpiringTime { get; set; }
	}
}
