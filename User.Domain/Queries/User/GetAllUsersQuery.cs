using MediatR;
using NetDevPack.Messaging;
using User.Domain.Models;

namespace User.Domain.Queries.User
{
    public class GetAllUsersQuery :Command, IRequest<IEnumerable<UserModel>>
    {
        public GetAllUsersQuery()
        {

        }
    }
}
