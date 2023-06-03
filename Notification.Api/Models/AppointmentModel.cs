using NetDevPack.Domain;
using Redis.OM.Modeling;

namespace Notification.Api.Models
{
	[Document(StorageType = StorageType.Json, Prefixes = new[] { "AppointmentModel" })]
	public class AppointmentModel:IAggregateRoot
	{
		[Indexed, RedisIdField]
		public Guid Id { get; set; }
		[Searchable]
		public string Message { get; set; }
		[Indexed]
		public Guid AppointmentId { get; set; }
		[Indexed]
		public string SendTo { get; set; }
	}
}
