using NetDevPack.Messaging;

namespace Events.User.Address
{
	public class UserAddressUpdatedEvent:Event
	{
		public UserAddressUpdatedEvent()
		{

		}

		public UserAddressUpdatedEvent(Guid id, Guid userId, string street, string city, string state, string country, string zipCode)
		{
			Id = id;
			UserId = userId;
			Street = street;
			City = city;
			State = state;
			Country = country;
			ZipCode = zipCode;
		}

		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
	}
}
