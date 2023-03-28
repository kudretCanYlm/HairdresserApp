using FluentValidation;
using User.Domain.Commands.Address;

namespace User.Domain.Validations.Address
{
	public abstract class UserAddressValidation<T> : AbstractValidator<T> where T : UserAddressCommand
	{
		protected void ValidateId()
		{
			RuleFor(x => x.Id)
				.NotEqual(Guid.Empty);
		}

		protected void ValidateUserId()
		{
			RuleFor(x => x.UserId)
				.NotEqual(Guid.Empty);
		}

		protected void ValidateStreet()
		{
			RuleFor(x => x.Street)
				.NotEmpty().WithMessage("Please ensure you have entered the {PropertyName}")
				.Length(2, 100).WithMessage("The {PropertyName} must have between {MinLength} and {MaxLength} characters");
		}

		protected void ValidateCity()
		{
			RuleFor(x => x.City)
				.NotEmpty().WithMessage("Please ensure you have entered the {PropertyName}")
				.Length(2, 100).WithMessage("The {PropertyName} must have between {MinLength} and {MaxLength} characters");
		}
		protected void ValidateState()
		{
			RuleFor(x => x.State)
				.NotEmpty().WithMessage("Please ensure you have entered the {PropertyName}")
				.Length(2, 100).WithMessage("The {PropertyName} must have between {MinLength} and {MaxLength} characters");
		}
		protected void ValidateCountry()
		{
			RuleFor(x => x.Country)
				.NotEmpty().WithMessage("Please ensure you have entered the {PropertyName}")
				.Length(2, 100).WithMessage("The {PropertyName} must have between {MinLength} and {MaxLength} characters");
		}
		
		protected void ValidateZipCode()
		{
			RuleFor(x => x.ZipCode)
				.NotEmpty().WithMessage("Please ensure you have entered the {PropertyName}")
				.Length(5).WithMessage("The {PropertyName} must have {TotalLength} characters");
		}

	}
}
