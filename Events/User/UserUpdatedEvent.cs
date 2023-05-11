using NetDevPack.Messaging;

namespace Events.User
{
	public class UserUpdatedEvent:Event
	{
		public UserUpdatedEvent()
		{

		}

		public UserUpdatedEvent(Guid id, string name, string surname, string email, string password, string phone)
		{
			AggregateId = id;
			Id = id;
			Name = name;
			Surname = surname;
			Email = email;
			Password = password;
			Phone = phone;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }
	}
}
