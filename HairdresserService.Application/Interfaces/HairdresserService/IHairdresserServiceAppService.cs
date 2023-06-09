﻿using FluentValidation.Results;
using HairdresserService.Application.Dto;
using HairdresserService.Application.EventSourcedNormalizers.HairdresserService;

namespace HairdresserService.Application.Interfaces.HairdresserService
{
	public interface IHairdresserServiceAppService:IDisposable
	{
		Task<HairdresserServiceDto> GetHairdresserServiceByIdAndHairdresserId(Guid id,Guid hairdresserId);
		Task<HairdresserServiceImageDto> GetAllHairdresserServiceWithImagesById(Guid serviceId);
		Task<IEnumerable<HairdresserServiceDto>> GetAllHairdresserServicesByHairdresserId(Guid hairdresserId);
		Task<ValidationResult> ActivateHairdresserService(ActivateHairdresserServiceDto activateHairdresserServiceDto);
		Task<ValidationResult> CreateHairdresserService(CreateHairdresserServiceDto createHairdresserServiceDto);
		Task<ValidationResult> DeactivateHairdresserService(ActivateHairdresserServiceDto activateHairdresserServiceDto);
		Task<ValidationResult> DeleteHairdresserService(DeleteHairdresserServiceDto deleteHairdresserServiceDto);
		Task<ValidationResult> UpdateHairdresserService(UpdateHairdresserServiceDto updateHairdresserServiceDto);
		Task<IEnumerable<HairdresserServiceHistoryData>> GetAllHistoryAsync(Guid id);

	}
}
