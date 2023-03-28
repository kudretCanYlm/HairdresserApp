using User.Domain.Commands.User;

namespace User.Domain.Validations.User
{
    public class CreateUserValidation : UserValidation<CreateUserCommand>
    {
        public CreateUserValidation()
        {
            ValidateName();
            ValidateSurname();
            ValidateEmail();
            ValidatePassword();
            ValidatePhone();
        }
    }
}
