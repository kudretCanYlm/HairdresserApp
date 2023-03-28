namespace User.Application.EventSourcedNormalizers.User
{
	public class UserHistoryData
	{
		public string Action { get; set; }
		public string Id { get; set; }
		public string aggregateId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Timestamp { get; set; }
		public string Who { get; set; }
	}
}
