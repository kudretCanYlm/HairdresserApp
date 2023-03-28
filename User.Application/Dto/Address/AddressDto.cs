namespace User.Application.Dto.Address
{
	public class AddressDto
	{
		public Guid Id { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
	}
}
