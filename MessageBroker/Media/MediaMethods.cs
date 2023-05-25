namespace Common.Media
{
	public class MediaMethods
	{
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
