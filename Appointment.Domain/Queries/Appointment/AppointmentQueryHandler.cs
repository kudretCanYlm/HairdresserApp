using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class AppointmentQueryHandler : IRequestHandler<GetAllAppointmentsByUserId, IEnumerable<AppointmentModel>>,
										IRequestHandler<GetAppointmentByIdAndUserId, AppointmentModel>
	{
		private readonly IAppointmentRepository _appointmentRepository;

		public AppointmentQueryHandler(IAppointmentRepository appointmentRepository)
		{
			_appointmentRepository = appointmentRepository;
		}

		public async Task<IEnumerable<AppointmentModel>> Handle(GetAllAppointmentsByUserId request, CancellationToken cancellationToken)
		{
			var appointments=await _appointmentRepository.GetAllAppointmentsByUserId(request.UserId);

			return appointments;
		}

		public async Task<AppointmentModel> Handle(GetAppointmentByIdAndUserId request, CancellationToken cancellationToken)
		{
			var appointment = await _appointmentRepository.GetAppointmentByIdAndUserId(request.Id, request.UserId);

			return appointment;
		}
	}
}
