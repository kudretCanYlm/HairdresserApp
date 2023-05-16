using Pipelines.Sockets.Unofficial.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hairdresser.Domain.Commands.Hairdresser
{
	public class DeleteHairdresserCommand:HairdresserCommand
	{
		public DeleteHairdresserCommand()
		{

		}

		public DeleteHairdresserCommand(Guid id, Guid ownerId)
		{
			Id = id;
			OwnerId = ownerId;
		}
	}
}
