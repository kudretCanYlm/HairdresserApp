using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using Appointment.Domain.Specifications;
using Appointment.Infrastructure.Context;
using Database.Infrastructure;
using Database.Repository;
using Database.Specifications;
using Events.Appointment.Enum;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.Repository.Appointment
{
	public class AppointmentRepository : RepositoryBase<AppointmentModel, AppointmentContext>, IAppointmentRepository
	{
		public AppointmentRepository(IDatabaseFactory<AppointmentContext> context) : base(context)
		{

		}

		public async Task<bool> CheckIsthereAnAppointment(Guid id, DateTime appointmentDate, TimeSpan appointmentStartTime, TimeSpan serviceDuration)
		{

			var appointmentHairdresserSpecification = new AppointmentHairdresserSpecification(id);
			var appointmentStateCanceledOrDeniedSpecification = new AppointmentStateCanceledOrDeniedSpecification();
			var AppointmentTimeConflictSpecification = new AppointmentTimeConflictSpecification(appointmentDate, appointmentStartTime, serviceDuration);

			var combineSpecification =
				appointmentHairdresserSpecification
				.And(appointmentStateCanceledOrDeniedSpecification)
				.And(AppointmentTimeConflictSpecification).Criteria;


			var count = await GetManyQuery(combineSpecification).CountAsync();

			return count == 0 ? false : true;
		}

		public async Task<IEnumerable<AppointmentModel>> GetAllAppointmentsByUserId(Guid userId)
		{
			return await GetManyQuery(x => x.UserId == userId)
				.OrderByDescending(x => x.ModifiedAt)
				.ThenByDescending(x => x.CreatedAt)
				.ToListAsync();
		}

		public async Task<AppointmentModel> GetAppointmentByIdAndHairdresserId(Guid id, Guid hairdresserId)
		{
			return await Get(x => x.Id == id && x.HairdresserId == hairdresserId);
		}

		public async Task<AppointmentModel> GetAppointmentByIdAndUserId(Guid id, Guid userId)
		{
			return await Get(x => x.Id == id && x.UserId == userId);
		}
	}
}
