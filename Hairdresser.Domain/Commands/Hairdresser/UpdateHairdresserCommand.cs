namespace Hairdresser.Domain.Commands.Hairdresser
{
	public class UpdateHairdresserCommand:HairdresserCommand
	{
		public UpdateHairdresserCommand()
		{

		}

		public UpdateHairdresserCommand(Guid id,string name, string about, string address, string coordinate, TimeSpan openHour, TimeSpan closeHour, string workDays, Guid ownerId)
		{
			Id = id;
			Name = name;
			About = about;
			Address = address;
			Coordinate = coordinate;
			OpenHour = openHour;
			CloseHour = closeHour;
			WorkDays = workDays;
			OwnerId = ownerId;
		}
	}
}
