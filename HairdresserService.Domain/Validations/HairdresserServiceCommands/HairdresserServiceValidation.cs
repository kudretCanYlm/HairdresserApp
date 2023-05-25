using FluentValidation;
using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Validations.HairdresserServiceCommands
{
	public abstract class HairdresserServiceValidation<T>:AbstractValidator<T> where T:HairdresserServiceCommand
	{
		protected void ValidateId()
		{
			RuleFor(x => x.Id)
				.NotEqual(Guid.Empty);
		}

		protected void ValidateName()
		{
			RuleFor(x => x.Name)
				.NotNull()
				.WithMessage("{PropertyName} cannot be null")
				.NotEqual(String.Empty)
				.WithMessage("{PropertyName} cannot be empty");
		}

		protected void ValidatePrice() 
		{
			RuleFor(x => x.Price)
				.GreaterThan(0)
				.WithMessage("{PropertyName} greater then 0");
		}

		protected void ValidateHairdresserId()
		{
			RuleFor(x => x.HairdresserId)
				.NotEqual(Guid.Empty);
		}


	}
}
