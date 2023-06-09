﻿using Appointment.Core.Constraints;
using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using Appointment.Domain.Specifications;
using Database.Specifications;
using Events.Appointment.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Domain.Queries.Appointment
{
	public class AppointmentQueryHandler : IRequestHandler<GetAllAppointmentsByUserIdQuery, IEnumerable<AppointmentModel>>,
										IRequestHandler<GetAppointmentByIdAndUserIdQuery, AppointmentModel>,
										IRequestHandler<GetAppointmentByIdQuery, AppointmentModel>,
										IRequestHandler<GetAllAppointmentsForUserQuery, IEnumerable<AppointmentModel>>,
										IRequestHandler<GetAllAppointmentsByHairdresserIdQuery, IEnumerable<AppointmentModel>>,
										IRequestHandler<GetCreatedAppointmentsByHairdresserIdQuery, IEnumerable<AppointmentModel>>,
										IRequestHandler<GetApprovedAppointmentsByHairdresserIdQuery, IEnumerable<AppointmentModel>>
	{
		private readonly IAppointmentRepository _appointmentRepository;

		public AppointmentQueryHandler(IAppointmentRepository appointmentRepository)
		{
			_appointmentRepository = appointmentRepository;
		}

		public async Task<IEnumerable<AppointmentModel>> Handle(GetAllAppointmentsByUserIdQuery request, CancellationToken cancellationToken)
		{
			var appointments = await _appointmentRepository.GetAllAppointmentsByUserId(request.UserId);

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

		public async Task<IEnumerable<AppointmentModel>> Handle(GetAllAppointmentsForUserQuery request, CancellationToken cancellationToken)
		{
			var inThisWeekAndGreaterThanNow = DateConstrains.InThisWeekAndGreaterThanNowConstrain(request.AppointmentDate);

			if (!inThisWeekAndGreaterThanNow)
				return null;

			var appointmentHairdresserSpecification = new AppointmentHairdresserSpecification(request.HairdresserId);
			var appointmentStateCanceledOrDeniedSpecification = new AppointmentStateCanceledOrDeniedSpecification();
			var appointmentEqualDateSpecification = new AppointmentEqualDateSpecification(request.AppointmentDate);


			var appointments = await _appointmentRepository.GetManyQuery(
				appointmentHairdresserSpecification
				.And(appointmentStateCanceledOrDeniedSpecification)
				.And(appointmentEqualDateSpecification).Criteria)
				.OrderBy(x=>x.AppointmentStartTime)
				.ToListAsync();

			return appointments;
		}

		public async Task<IEnumerable<AppointmentModel>> Handle(GetAllAppointmentsByHairdresserIdQuery request, CancellationToken cancellationToken)
		{
			var appointmentHairdresserSpecification = new AppointmentHairdresserSpecification(request.HairdresserId);

			var appointmets=await _appointmentRepository.GetManyQuery(
				appointmentHairdresserSpecification.Criteria)
				.OrderByDescending(x=>x.ModifiedAt)
				.ThenByDescending(x=>x.CreatedAt) 
				.ToListAsync();
			
			return appointmets;

		}

		public async Task<IEnumerable<AppointmentModel>> Handle(GetCreatedAppointmentsByHairdresserIdQuery request, CancellationToken cancellationToken)
		{
			var appointmentHairdresserSpecification = new AppointmentHairdresserSpecification(request.HairdresserId);
			var appointmentSingleStateSpecification = new AppointmentSingleStateSpecification(AppointmentStateEnum.Created);

			var appointments = await _appointmentRepository.GetManyQuery(
				appointmentHairdresserSpecification
				.And(appointmentSingleStateSpecification).Criteria)
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();

			return appointments;
		}

		public async Task<IEnumerable<AppointmentModel>> Handle(GetApprovedAppointmentsByHairdresserIdQuery request, CancellationToken cancellationToken)
		{
			var appointmentHairdresserSpecification = new AppointmentHairdresserSpecification(request.HairdresserId);
			var appointmentSingleStateSpecification = new AppointmentSingleStateSpecification(AppointmentStateEnum.Approved);
			var appointmentEqualDateSpecification = new AppointmentEqualDateSpecification(DateTime.Now);
			var appointmentEqualTimeSpecification = new AppointmentEqualTimeSpecification(DateTime.Now);

			var appointments = await _appointmentRepository.GetManyQuery(
				appointmentHairdresserSpecification
				.And(appointmentSingleStateSpecification)
				.And(appointmentEqualDateSpecification)
				.And(appointmentEqualTimeSpecification)
				.Criteria)
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();

			return appointments;
		}
	}
}
