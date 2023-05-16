using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdresser.Domain.Commands.Hairdresser
{
	public class CreateHairdresserCommand : HairdresserCommand
	{
		public CreateHairdresserCommand()
		{

		}

		public CreateHairdresserCommand(string name,string about,string address,string coordinate, TimeSpan openHour, TimeSpan closeHour,string workDays,Guid ownerId)
		{
			Name=name;
			About=about;
			Address = address;
			Coordinate = coordinate;
			OpenHour = openHour;
			CloseHour = closeHour;
			WorkDays = workDays;
			OwnerId = ownerId;
		}
	}
}
