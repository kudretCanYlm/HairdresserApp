using Media.Application.Dto;
using Media.Application.EventSourcedNormalizers.Media;
using FluentValidation.Results;

namespace Media.Application.Interfaces.Media
{
	public interface IMediaAppService:IDisposable
	{
		Task<IEnumerable<MediaDto>> GetAllByOwnerIdAsync(Guid ownerId);
		Task<MediaDto> GetByOwnerIdAndImageType(Guid ownerId, string type);
		Task<ValidationResult> CreateAsync(CreateMediaDto createMediaDto);
		Task<ValidationResult> UpdateAsync(UpdateMediaDto updateMediaDto);
		Task<ValidationResult> RemoveAsync(Guid id);
		Task<IList<MediaHistoryData>> GetAllHistoryAsync(Guid id);
	}
}
