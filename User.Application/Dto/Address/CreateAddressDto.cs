namespace User.Application.Dto.Address
{
	public class CreateAddressDto
	{
		public Guid UserId { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
	}
}
