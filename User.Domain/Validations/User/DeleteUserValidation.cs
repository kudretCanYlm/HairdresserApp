using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Commands.User;

namespace User.Domain.Validations.User
{
    public class DeleteUserValidation : UserValidation<DeleteUserCommand>
    {
        public DeleteUserValidation()
        {
            ValidateId();
        }
    }
}
