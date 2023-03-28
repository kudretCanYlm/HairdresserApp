namespace User.Application.EventSourcedNormalizers.Address
{
	public class AddressHistoryData
	{
		public string Action { get; set; }
		public string Id { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
		public string Timestamp { get; set; }
		public string Who { get; set; }
	}
}
