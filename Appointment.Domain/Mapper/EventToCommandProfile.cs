using Appointment.Domain.Commands.Appointment;
using AutoMapper;
using Events.Appointment;

namespace Appointment.Domain.Mapper
{
	public class EventToCommandProfile:Profile
	{
		public EventToCommandProfile()
		{
			CreateMap<AppointmentApprovedEvent, ApproveAppointmentCommand>().ReverseMap();
			CreateMap<AppointmentCanceledEvent, CancelAppointmentCommand>().ReverseMap();
			CreateMap<AppointmentCompletedEvent, CompleteAppointmentCommand>().ReverseMap();
			CreateMap<AppointmentCreatedEvent, CreateAppointmentCommand>().ReverseMap();
			CreateMap<AppointmentDeniedEvent, DenyAppointmentCommand>().ReverseMap();
			CreateMap<AppointmentInProcessedEvent, InProcessAppointmentCommand>().ReverseMap();
			CreateMap<AppointmentUpdatedEvent, UpdateAppointmentCommand>().ReverseMap();
		}
	}
}
