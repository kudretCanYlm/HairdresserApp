using User.Domain.Validations.User;

namespace User.Domain.Commands.User
{
    public class UpdateUserCommand : UserCommand
    {
        public UpdateUserCommand(Guid id, string name, string surname, string email, string password, string phone)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Phone = phone;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
