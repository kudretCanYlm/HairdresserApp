using FluentValidation;
using Hairdresser.Domain.Commands.Hairdresser;

namespace Hairdresser.Domain.Validations.HairdresserCommands
{
	public abstract class HairdresserValidation<T> : AbstractValidator<T> where T : HairdresserCommand
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
				.MinimumLength(5)
				.WithMessage("{PropertyName} length greater then or equal 5")
				.MaximumLength(50)
				.WithMessage("{PropertyName} length lesser then or equal 50")
				.NotEqual(String.Empty)
				.WithMessage("{PropertyName} cannot be empty");
		}

		protected void ValidateAbout()
		{
			RuleFor(x => x.About)
				.MinimumLength(20)
				.WithMessage("{PropertyName} length greater then or equal 20")
				.MaximumLength(250)
				.WithMessage("{PropertyName} length lesser then or equal 250");
		}

		protected void ValidateAddress()
		{
			RuleFor(x => x.About)
				.NotNull()
				.WithMessage("{PropertyName} cannot be null")
				.MinimumLength(5)
				.WithMessage("{PropertyName} length greater then or equal 5")
				.MaximumLength(500)
				.WithMessage("{PropertyName} length lesser then or equal 500")
				.NotEqual(String.Empty)
				.WithMessage("{PropertyName} cannot be empty");
		}

		protected void ValidateCoordinate()
		{
			RuleFor(x => x.Coordinate)
				.NotNull()
				.WithMessage("{PropertyName} cannot be null")
				.NotEqual(String.Empty)
				.WithMessage("{PropertyName} cannot be empty");
		}

		protected void ValidateOwnerId()
		{
			RuleFor(x => x.OwnerId)
				.NotEqual(Guid.Empty);
		}

	}
}
