using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using Appointment.Infrastructure.Context;
using Database.Infrastructure;
using Database.Repository;
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

			var query = GetManyQuery(x =>

				(
				//test later
				x.AppointmentState!=AppointmentStateEnum.Denied
				||
				x.AppointmentState!=AppointmentStateEnum.Cancelled
				)
				&&
				x.AppointmentDate.Date == appointmentDate.Date
				&&
				(
				       (
				       appointmentStartTime >= x.AppointmentStartTime
				       &&
				       appointmentStartTime <= x.AppointmentEndTime
				       )
			        ||
			           (
				       appointmentStartTime.Add(serviceDuration) >= x.AppointmentStartTime 
				       &&
				       appointmentStartTime.Add(serviceDuration) <= x.AppointmentEndTime
			           )
			));

			return await query.CountAsync() == 0 ? false : true;
		}

		private bool IsBetweenTimeSpans(double targetTimeSpan, double startTimeSpan, double endTimeSpan)
		{
			return targetTimeSpan >= startTimeSpan && targetTimeSpan <= endTimeSpan;
		}

		public async Task<IEnumerable<AppointmentModel>> GetAllAppointmentsByUserId(Guid userId)
		{
			return await GetMany(x => x.UserId == userId);
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
