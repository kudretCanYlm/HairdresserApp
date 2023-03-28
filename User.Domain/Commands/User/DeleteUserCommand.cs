using User.Domain.Validations.User;

namespace User.Domain.Commands.User
{
    public class DeleteUserCommand : UserCommand
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
