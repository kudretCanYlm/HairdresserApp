using NetDevPack.Messaging;

namespace Events.Media
{
	public class MediaCreatedEvent:Event
	{
		public MediaCreatedEvent()
		{

		}

		public MediaCreatedEvent(Guid id, string fileExtension, byte[] mediaData, string customType, Guid ımageOwnerId)
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
