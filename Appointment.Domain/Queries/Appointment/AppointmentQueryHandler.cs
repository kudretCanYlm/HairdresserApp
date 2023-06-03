using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class AppointmentQueryHandler : IRequestHandler<GetAllAppointmentsByUserIdQuery, IEnumerable<AppointmentModel>>,
										IRequestHandler<GetAppointmentByIdAndUserIdQuery, AppointmentModel>,
										IRequestHandler<GetAppointmentByIdQuery, AppointmentModel>
	{
		private readonly IAppointmentRepository _appointmentRepository;

		public AppointmentQueryHandler(IAppointmentRepository appointmentRepository)
		{
			_appointmentRepository = appointmentRepository;
		}

		public async Task<IEnumerable<AppointmentModel>> Handle(GetAllAppointmentsByUserIdQuery request, CancellationToken cancellationToken)
		{
			var appointments=await _appointmentRepository.GetAllAppointmentsByUserId(request.UserId);

			return appointments;
		}

		public async Task<AppointmentModel> Handle(GetAppointmentByIdAndUserIdQuery request, CancellationToken cancellationToken)
		{
			var appointment = await _appointmentRepository.GetAppointmentByIdAndUserId(request.Id, request.UserId);

			return appointment;
		}

		public async Task<AppointmentModel> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
		{
			var appointment = await _appointmentRepository.GetById(request.Id);

			return appointment;
		}
	}
}
