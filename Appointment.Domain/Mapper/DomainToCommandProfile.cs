using Appointment.Domain.Commands.Appointment;
using Appointment.Domain.Models;
using AutoMapper;
using Events.Appointment;

namespace Appointment.Domain.Mapper
{
	public class DomainToCommandProfile:Profile
	{
		public DomainToCommandProfile()
		{
			CreateMap<ApproveAppointmentCommand, AppointmentModel>().ReverseMap();
			CreateMap<CancelAppointmentCommand, AppointmentModel>().ReverseMap();
			CreateMap<CompleteAppointmentCommand, AppointmentModel>().ReverseMap();
			CreateMap<CreateAppointmentCommand, AppointmentModel>().ReverseMap();
			CreateMap<DenyAppointmentCommand, AppointmentModel>().ReverseMap();
			CreateMap<InProcessAppointmentCommand, AppointmentModel>().ReverseMap();
			CreateMap<UpdateAppointmentCommand, AppointmentModel>().ReverseMap();

			CreateMap<AppointmentModel,AppointmentApprovedEvent>().ReverseMap();
			CreateMap<AppointmentModel,AppointmentCanceledEvent>().ReverseMap();
			CreateMap<AppointmentModel,AppointmentCompletedEvent>().ReverseMap();
			CreateMap<AppointmentModel,AppointmentCreatedEvent>().ReverseMap();
			CreateMap<AppointmentModel,AppointmentDeniedEvent>().ReverseMap();
			CreateMap<AppointmentModel,AppointmentInProcessedEvent>().ReverseMap();
			CreateMap<AppointmentModel, AppointmentUpdatedEvent>().ReverseMap();
		}
	}
}
