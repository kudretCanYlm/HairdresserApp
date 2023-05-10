namespace Media.Application.Dto
{
	public class UpdateMediaDto
	{
		//can change
		public Guid Id { get; set; }
		public string Base64Media { get; set; }
		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
