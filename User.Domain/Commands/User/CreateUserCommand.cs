using User.Domain.Validations.User;

namespace User.Domain.Commands.User
{
    public class CreateUserCommand : UserCommand
    {
        public CreateUserCommand(string name, string surname, string email, string password, string phone)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Phone = phone;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
