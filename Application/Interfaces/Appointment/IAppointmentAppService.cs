using Appointment.Application.Dto;
using Appointment.Application.EventSourcedNormalizers.Appointment;
using FluentValidation.Results;

namespace Appointment.Application.Interfaces.Appointment
{
	public interface IAppointmentAppService:IDisposable
	{
		Task<IEnumerable<AppointmentDto>> GetAllAppointmentsByUserId(Guid userId);
		Task<AppointmentDto> GetAppointmentByIdAndUserId(Guid id,Guid userId);
		Task<ValidationResult> ApproveAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto);
		Task<ValidationResult> CancelAppointment(AppointmentStateUserDto appointmentStateUserDto);
		Task<ValidationResult> CompleteAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto);
		Task<ValidationResult> CreateAppointment(CreateAppointmentDto createAppointmentDto);
		Task<ValidationResult> DenyAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto);
		Task<ValidationResult> InProcessAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto);
		Task<ValidationResult> UpdateAppointment(UpdateAppointmentDto updateAppointmentDto);
		Task<IList<AppointmentHistoryData>> GetAllHistoryAsync(Guid id);

	}
}
