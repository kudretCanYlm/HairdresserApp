using MediatR;
using NetDevPack.Messaging;
using User.Domain.Models;

namespace User.Domain.Queries.User
{
    public class GetAllUsersQuery :IRequest<IEnumerable<UserModel>>
    {
        public GetAllUsersQuery()
        {

        }
    }
}
