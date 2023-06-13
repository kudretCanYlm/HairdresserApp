using Appointment.Application.Dto;
using Appointment.Domain.Commands.Appointment;
using Appointment.Domain.Queries.Appointment;
using AutoMapper;


namespace Appointment.Application.Mapper
{
	public class DtoToCommand:Profile
	{
		public DtoToCommand()
		{
			CreateMap<AppointmentStateHairdresserDto, ApproveAppointmentCommand>();
			CreateMap<AppointmentStateHairdresserDto, CompleteAppointmentCommand>();
			CreateMap<AppointmentStateHairdresserDto, DenyAppointmentCommand>();
			CreateMap<AppointmentStateHairdresserDto, InProcessAppointmentCommand>();

			CreateMap<AppointmentStateUserDto, CancelAppointmentCommand>();
			CreateMap<AppointmentStateUserDto, CancelAppointmentCommand>();

			CreateMap<CreateAppointmentDto, CreateAppointmentCommand>();
			CreateMap<UpdateAppointmentDto, UpdateAppointmentCommand>();

			CreateMap<GetAllAppointmentsForUserPostDto, GetAllAppointmentsForUserQuery>();

		}
	}
}
