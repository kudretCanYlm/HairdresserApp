using NetDevPack.Messaging;

namespace Media.Domain.Commands.Media
{
	public abstract class MediaCommand : Command
	{
		public Guid Id { get; set; }
		public string FileExtension { get; set; }
		public byte[] MediaData { get; set; }
		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
