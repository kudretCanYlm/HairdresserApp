using Appointment.Domain.Commands.Appointment;
using FluentValidation;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public abstract class AppointmentValidation<T> : AbstractValidator<T> where T : AppointmentCommand
	{
		protected void ValidateId()
		{
			RuleFor(x => x.Id)
				.NotEqual(Guid.Empty);
		}

		protected void ValidateNotes()
		{
			RuleFor(x => x.Notes)
				.MaximumLength(500)
				.WithMessage("{PropertyName} lesser than 500 character or equal");
		}

		protected void ValidateAppointmentDate()
		{
			RuleFor(x => x.AppointmentDate)
				.NotNull()
				.WithMessage("{PropertyName} don't be null")
				.NotEmpty()
				.WithMessage("{PropertyName} don't be empty");
		}

		protected void ValidateAppointmentStartTime()
		{
			RuleFor(x => x.AppointmentStartTime)
				.NotNull()
				.WithMessage("{PropertyName} don't be null")
				.NotEmpty()
				.WithMessage("{PropertyName} don't be empty");
		}

		protected void ValidateUserId() 
		{
			RuleFor(x => x.UserId)
				.NotEqual(Guid.Empty);
		}
		protected void ValidateHairdresserServiceId() 
		{
			RuleFor(x => x.HairdresserServiceId)
				.NotEqual(Guid.Empty);
		}
		protected void ValidateHairdresserId() 
		{
			RuleFor(x => x.HairdresserId)
				.NotEqual(Guid.Empty);
		}

	}
}
