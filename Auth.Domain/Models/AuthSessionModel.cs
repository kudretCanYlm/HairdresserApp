using NetDevPack.Domain;
using Redis.OM.Modeling;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Domain.Models
{
	[Document(StorageType = StorageType.Json, Prefixes = new[] { "AuthSessionModel" })]
	public class AuthSessionModel :IAggregateRoot
	{
		public AuthSessionModel()
		{
			Id = Guid.NewGuid();
		}

		public AuthSessionModel(Guid tokenOwnerId, DateTime tokenExpiringTime)
		{
			Id = Guid.NewGuid();
			TokenOwnerId = tokenOwnerId;
			TokenExpiringTime = tokenExpiringTime;
		}

		[Indexed, RedisIdField]
		public Guid Id { get; set; }

		[Searchable]
		public string Token { get; set; }
		[Indexed]
		public Guid TokenOwnerId { get; set; }
		
		public DateTime TokenExpiringTime { get; set; }

		public AuthSessionModel GenerateToken()
		{
			var token = TokenOwnerId.ToString() + DateTime.Now.ToString() + Id.ToString();
			var tokenBytes = Encoding.ASCII.GetBytes(token);
			StringBuilder generatedToken = new StringBuilder();

			using (var sha512 = SHA512.Create())
			{
				byte[] hashValue = sha512.ComputeHash(tokenBytes);

				foreach (byte b in hashValue)
					generatedToken.Append(b.ToString("x2"));
			}

			Token = generatedToken.ToString();
			return this;
		}
	}
}
