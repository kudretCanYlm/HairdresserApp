using AutoMapper;
using Events.User;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using User.Domain.Interfaces;
using User.Domain.Models;

namespace User.Domain.Commands.User
{
	public class UserCommandHandler : CommandHandler,
									IRequestHandler<CreateUserCommand, ValidationResult>,
									IRequestHandler<DeleteUserCommand, ValidationResult>,
									IRequestHandler<UpdateUserCommand, ValidationResult>
	{
		private readonly IUserRepository userRepository;
		private readonly IMapper mapper;

		public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
		{
			this.userRepository = userRepository;
			this.mapper = mapper;
		}

		public async Task<ValidationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = mapper.Map<UserModel>(request);

			if (await userRepository.GetByEmail(request.Email) != null)
			{
				AddError("The user e-mail has already been taken.");
				return ValidationResult;
			}
			
			userRepository.Add(user);
			user.AddDomainEvent(mapper.Map<UserCreatedEvent>(user));

			
			return await Commit(userRepository.UnitOfWork);
		}

		public async Task<ValidationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var user = await userRepository.GetById(request.Id);

			if (user is null)
			{
				AddError("The customer doesn't exists.");
				return ValidationResult;
			}

			user.AddDomainEvent(mapper.Map<UserDeletedEvent>(user));

			userRepository.Delete(user);

			return await Commit(userRepository.UnitOfWork);

		}

		public async Task<ValidationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var user = await userRepository.GetById(request.Id);

			user = mapper.Map(request, user);

			if (user == null)
			{
				AddError("user not found");
				return ValidationResult;
			}

			if (await userRepository.IsEmailAlreadyUsing(request.Email))
			{
				AddError("The user e-mail has already been taken.");
				return ValidationResult;
			}

			user.AddDomainEvent(mapper.Map<UserUpdatedEvent>(user));

			userRepository.Update(user);

			return await Commit(userRepository.UnitOfWork);

		}
	}
}
