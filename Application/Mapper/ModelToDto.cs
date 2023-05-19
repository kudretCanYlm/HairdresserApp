using Appointment.Application.Dto;
using Appointment.Domain.Models;
using AutoMapper;

namespace Appointment.Application.Mapper
{
	public class ModelToDto:Profile
	{
		public ModelToDto()
		{
			CreateMap<AppointmentModel, AppointmentDto>();
		}
	}
}
