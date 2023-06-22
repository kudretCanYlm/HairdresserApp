using Database.PaggingAndFilter;
using FluentValidation.Results;
using Hairdresser.Application.Dto;
using Hairdresser.Application.EventSourcedNormalizers.Hairdresser;

namespace Hairdresser.Application.Interfaces.Hairdresser
{
	public interface IHairdresserAppService: IDisposable
	{
		Task<bool> CheckHairdresserActive(Guid id, DateTime appointmentDate, TimeSpan appointmentStartTime, TimeSpan serviceDuration);
		Task<bool> CheckHairdresserByIdAndUserId(Guid id,Guid userId);
		Task<IEnumerable<HairdresserImageDto>> GetAllHairdresserByFilter(PageSearchArgs pageSearchArgs);
		Task<IEnumerable<HairdresserDto>> GetAllHairdressers();
		Task<HairdresserDto> GetHairdresserById(Guid id);
		Task<HairdresserImageDto> GetHairdresserWithImageById(Guid id);
		//Task<IEnumerable<HairdresserWithImageDto>> GetHairdresserByPagedList(PageSearchArgs args);
		Task<ValidationResult> CreateAsync(CreateHairdresserDto createHairdresserDto);
		Task<ValidationResult> UpdateAsync(UpdateHairdresserDto updateHairdresserDto);
		Task<ValidationResult> RemoveAsync(Guid id,Guid OwnerId);
		Task<IList<HairdresserHistoryData>> GetAllHistoryAsync(Guid id);
	}
}
