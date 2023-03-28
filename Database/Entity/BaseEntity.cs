namespace Database.Entity
{
	public abstract class BaseEntity: NetDevPack.Domain.Entity, IBaseEntity
	{
		public DateTime? CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public Guid? DeletedBy { get; set; }
		public Guid? ModifiedBy { get; set; }
		public Guid? CreatedBy { get; set; }
		public bool isDeleted { get; set; }
	}
}
