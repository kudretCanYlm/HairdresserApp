﻿using Events.Appointment.Enum;

namespace Appointment.Domain.Commands.Appointment
{
	public class CreateAppointmentCommand:AppointmentCommand
	{
		public CreateAppointmentCommand()
		{
			AppointmentState = AppointmentStateEnum.Waiting;
		}

		public CreateAppointmentCommand(string notes, DateTime appointmentDate, TimeSpan appointmentStartTime, Guid userId, Guid hairdresserServiceId, Guid hairdresserId)
		{
			AppointmentState= AppointmentStateEnum.Waiting;
			Notes= notes;
			AppointmentDate= appointmentDate;
			AppointmentStartTime= appointmentStartTime;
			UserId= userId;
			HairdresserServiceId= hairdresserServiceId;
			HairdresserId= hairdresserId;
		}

	}
}
