using Database.Entity;
using NetDevPack.Domain;

namespace Media.Domain.Models
{
	public class MediaModel:BaseEntity,IAggregateRoot
	{
		public MediaModel()
		{
			Id= Guid.NewGuid();
		}

		public string FileExtension { get; set; }
		public byte[] MediaData { get; set; }
		/// <summary>
		/// UserImage,HairdresserImage,HairdresserServicesImage etc.
		/// </summary>
		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }

		public static string ToBase64(MediaModel mediaModel)
		{
			string base64 = "data:" + mediaModel.FileExtension + ";base64," + Convert.ToBase64String(mediaModel.MediaData);

			return base64;
		}

		public static string ToFileExtension(string base64)
		{
			return base64.Split(';')[0].Substring(5);
		}

		public static byte[] ToByteArray(string base64)
		{
			return Convert.FromBase64String(base64.Split(',')[1]);
		}
	}
}
