namespace Media.Application.EventSourcedNormalizers.Media
{
	public class MediaHistoryData
	{
		public string Action { get; set; }


		public string Id { get; set; }

		public string FileExtension { get; set; }
		public byte[] MediaData { get; set; }

		public string CustomType { get; set; }
		public string ImageOwnerId { get; set; }
		public string Timestamp { get; set; }
		public string Who { get; set; }
	}
}
