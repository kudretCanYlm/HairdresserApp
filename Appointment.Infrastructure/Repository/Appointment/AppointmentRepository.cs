using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using Appointment.Infrastructure.Context;
using Database.Infrastructure;
using Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Infrastructure.Repository.Appointment
{
	public class AppointmentRepository:RepositoryBase<AppointmentModel,AppointmentContext>,IAppointmentRepository
	{
		public AppointmentRepository(IDatabaseFactory<AppointmentContext> context):base(context)
		{

		}

		public async Task<IEnumerable<AppointmentModel>> GetAllAppointmentsByUserId(Guid userId)
		{
			return await GetMany(x=>x.UserId==userId);
		}

		public async Task<AppointmentModel> GetAppointmentByIdAndHairdresserId(Guid id, Guid hairdresserId)
		{
			return await Get(x=>x.Id==id && x.HairdresserId==hairdresserId);
		}

		public async Task<AppointmentModel> GetAppointmentByIdAndUserId(Guid id, Guid userId)
		{
			return await Get(x=>x.Id== id && x.UserId==userId);
		}
	}
}
