using NetDevPack.Messaging;

namespace Events.User
{
	public class UserCreatedEvent:Event
	{
		public UserCreatedEvent()
		{
			
		}

		public UserCreatedEvent(Guid id, string name, string surname, string email, string password, string phone)
		{
			Id = id;
			AggregateId= id;
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
