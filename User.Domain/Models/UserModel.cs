using Database.Entity;
using NetDevPack.Domain;
using System.Security.Cryptography;
using System.Text;

namespace User.Domain.Models
{
	public class UserModel:BaseEntity,IAggregateRoot
	{
		public UserModel()
		{
			Id = Guid.NewGuid();
		}

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }

		public void ToHashPassword()
		{
			// Create a SHA256   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.Password));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}

				this.Password = builder.ToString();
			}
		}

		public static string ToHash(string value)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}

				return builder.ToString();
			}
		}

		

	}
}
