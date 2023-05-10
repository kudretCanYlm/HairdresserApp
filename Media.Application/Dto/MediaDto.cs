namespace Media.Application.Dto
{
	public class MediaDto
	{
		public Guid Id { get; set; }
		public string Base64Media { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
