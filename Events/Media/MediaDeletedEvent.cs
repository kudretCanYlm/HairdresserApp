using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Media
{
	public class MediaDeletedEvent:Event
	{
		public MediaDeletedEvent(Guid id)
		{
			Id = id;
			AggregateId= id;
		}

		public Guid Id { get; set; }
	}
}
