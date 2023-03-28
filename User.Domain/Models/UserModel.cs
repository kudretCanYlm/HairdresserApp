using Database.Entity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;

namespace User.Domain.Models
{
	[PrimaryKey(nameof(Id))]
	public class UserModel:BaseEntity,IAggregateRoot
	{
		public UserModel()
		{
			Id = Guid.NewGuid();
		}

		public UserModel(string name, string surname, string email, string password, string phone)
		{
			Id=Guid.NewGuid();
			Name = name;
			Surname = surname;
			Email = email;
			Password = password;
			Phone = phone;
		}
		
		public UserModel(Guid id,string name, string surname, string email, string password, string phone)
		{
			Id = id;
			Name = name;
			Surname = surname;
			Email = email;
			Password = password;
			Phone = phone;
		}

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }

	}
}
