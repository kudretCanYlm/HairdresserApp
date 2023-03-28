using User.Domain.Commands.User;

namespace User.Domain.Validations.User
{
    public class UpdateUserValidation : UserValidation<UpdateUserCommand>
    {
        public UpdateUserValidation()
        {
            ValidateId();
            ValidateName();
            ValidateSurname();
            ValidateEmail();
            ValidatePassword();
            ValidatePhone();
        }
    }
}
