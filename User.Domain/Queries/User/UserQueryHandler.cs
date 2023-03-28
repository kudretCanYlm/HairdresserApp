using MediatR;
using User.Domain.Interfaces;
using User.Domain.Models;

namespace User.Domain.Queries.User
{
    public class UserQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>,
                                    IRequestHandler<GetUserByIdQuery,UserModel>
    {
        private readonly IUserRepository userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAll();
            return users;
        }

		public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await userRepository.GetById(request.Id);

			if (user == null)
				throw new ArgumentNullException(nameof(user));
			return user;
		}
	}
}
