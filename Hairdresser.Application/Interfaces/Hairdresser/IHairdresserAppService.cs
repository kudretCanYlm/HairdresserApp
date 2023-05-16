using FluentValidation.Results;
using Hairdresser.Application.Dto;
using Hairdresser.Application.EventSourcedNormalizers.Hairdresser;

namespace Hairdresser.Application.Interfaces.Hairdresser
{
	public interface IHairdresserAppService: IDisposable
	{
		Task<IEnumerable<HairdresserDto>> GetAllHairdressers();
		Task<HairdresserDto> GetHairdresserById(Guid id);
		//Task<IEnumerable<HairdresserWithImageDto>> GetHairdresserByPagedList(PageSearchArgs args);
		Task<ValidationResult> CreateAsync(CreateHairdresserDto createHairdresserDto);
		Task<ValidationResult> UpdateAsync(UpdateHairdresserDto updateHairdresserDto);
		Task<ValidationResult> RemoveAsync(Guid id,Guid OwnerId);
		Task<IList<HairdresserHistoryData>> GetAllHistoryAsync(Guid id);
	}
}
