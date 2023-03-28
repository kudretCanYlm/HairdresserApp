using Database.Entity;
using NetDevPack.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.Domain.Models
{
	public class AddressModel:BaseEntity,IAggregateRoot
	{

		public AddressModel()
		{

		}

		public AddressModel(string street, string city, string state, string country, string zipCode)
		{
			Street = street;
			City = city;
			State = state;
			Country = country;
			ZipCode = zipCode;
		}
		public Guid UserId { get; set; }
		public string Street { get;  set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
	}
}
