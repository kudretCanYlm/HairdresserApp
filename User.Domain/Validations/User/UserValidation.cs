using FluentValidation;
using User.Domain.Commands.User;

namespace User.Domain.Validations.User
{
    public abstract class UserValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the {PropertyName}")
                .Length(2, 150).WithMessage("The {PropertyName} must have between {MinLength} and {MaxLength} characters");
        }
        protected void ValidateSurname()
        {
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Please ensure you have entered the {PropertyName}")
                .Length(2, 150).WithMessage("The {PropertyName} must have between {MinLength} and {MaxLength} characters");
        }

        protected void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidatePassword()
        {
            RuleFor(x => x.Password)
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
                .WithMessage("Minimum eight characters, at least one letter and one number");
        }

        protected void ValidatePhone()
        {
            RuleFor(x => x.Phone)
                .Matches("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$")
                .WithMessage("Enter valid phone number");
        }

    }
}
