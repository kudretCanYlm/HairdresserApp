using Database.Repository;
using Media.Domain.Models;

namespace Media.Domain.Interfaces
{
	public interface IMediaRepository: IBaseRepository<MediaModel>
	{
		Task<IReadOnlyList<MediaModel>> GetAllMediasByImageOwnerId(Guid imageOwnerId);
		Task<MediaModel> GetMediaByImageOwnerIdAndCustomType(Guid imageOwnerId, string imageType);
	}
}
