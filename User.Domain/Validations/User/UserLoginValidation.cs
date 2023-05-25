using FluentValidation;
using User.Domain.Queries.User;

namespace User.Domain.Validations.User
{
	public class UserLoginValidation : AbstractValidator<UserLoginQuery>
	{
		public UserLoginValidation()
		{
			RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();

			RuleFor(x => x.Password)
			.Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
			.WithMessage("Minimum eight characters, at least one letter and one number");
		}
	}
}
