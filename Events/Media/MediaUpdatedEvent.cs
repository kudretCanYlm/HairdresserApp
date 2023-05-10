using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Media
{
	public class MediaUpdatedEvent : Event
	{
		public MediaUpdatedEvent()
		{

		}

		public MediaUpdatedEvent(Guid id, string fileExtension, byte[] mediaData, string customType, Guid ımageOwnerId)
		{
			Id = id;
			FileExtension = fileExtension;
			MediaData = mediaData;
			CustomType = customType;
			ImageOwnerId = ımageOwnerId;
		}

		public Guid Id { get; set; }
		public string FileExtension { get; set; }
		public byte[] MediaData { get; set; }
		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
