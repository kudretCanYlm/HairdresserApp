using Database.Entity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;

namespace User.Domain.Models
{
	public class UserModel:BaseEntity,IAggregateRoot
	{
		public UserModel()
		{
			Id = Guid.NewGuid();
		}

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }

	}
}
