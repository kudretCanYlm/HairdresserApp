namespace Media.Application.Dto
{
	public class CreateMediaDto
	{
		public string Base64Media { get; set; }
		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
