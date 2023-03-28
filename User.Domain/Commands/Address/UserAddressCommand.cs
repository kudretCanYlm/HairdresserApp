using NetDevPack.Messaging;

namespace User.Domain.Commands.Address
{
	public abstract class UserAddressCommand:Command
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
	}
}
