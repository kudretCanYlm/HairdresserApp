using Appointment.Domain.Models;
using Database.Extensions;
using Database.Infrastructure;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Infrastructure.Context
{
	public class AppointmentContext : DbContext, IBaseDbContext
	{
		private readonly IMediatorHandler mediatorHandler;

		public AppointmentContext(DbContextOptions<AppointmentContext> options, IMediatorHandler mediatorHandler) : base(options)
		{
			this.mediatorHandler = mediatorHandler;
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			ChangeTracker.AutoDetectChangesEnabled = false;
		}

		public DbSet<AppointmentModel> Appointment { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//dont reflect to database
			modelBuilder.Ignore<ValidationResult>();
			modelBuilder.Ignore<Event>();

			base.OnModelCreating(modelBuilder);
		}

		public Task BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Commit()
		{
			//await 
			mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
			var success = await SaveChangesAsync() > 0;

			return success;
		}

		public Task RollBack()
		{
			throw new NotImplementedException();
		}
	}
}
