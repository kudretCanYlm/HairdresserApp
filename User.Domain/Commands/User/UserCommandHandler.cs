using AutoMapper;
using Common.Media;
using Events.MassTransitOptions;
using Events.Media;
using Events.User;
using FluentValidation.Results;
using MassTransit;
using MediatR;
using NetDevPack.Messaging;
using User.Domain.Interfaces;
using User.Domain.Models;

namespace User.Domain.Commands.User
{
	public class UserCommandHandler : CommandHandler,
									IRequestHandler<CreateUserCommand, FluentValidation.Results.ValidationResult>,
									IRequestHandler<DeleteUserCommand, FluentValidation.Results.ValidationResult>,
									IRequestHandler<UpdateUserCommand, FluentValidation.Results.ValidationResult>
	{
		private readonly IUserRepository userRepository;
		private readonly IMapper mapper;
		private readonly ISendEndpointProvider _sendEndpointProvider;

		public UserCommandHandler(IUserRepository userRepository, IMapper mapper, ISendEndpointProvider sendEndpointProvider)
		{
			this.userRepository = userRepository;
			this.mapper = mapper;
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = mapper.Map<UserModel>(request);

			if (await userRepository.GetByEmail(request.Email) != null)
			{
				AddError("The user e-mail has already been taken.");
				return ValidationResult;
			}

			user.ToHashPassword();

			userRepository.Add(user);
			user.AddDomainEvent(mapper.Map<UserCreatedEvent>(user));

			if (request.Base64Media.Length != 0)
			{
				var mediaCreatedEvent = new UserMediaCreatedEvent
				{
					Id = Guid.NewGuid(),
					FileExtension = MediaMethods.ToFileExtension(request.Base64Media),
					CustomType = MediaTypes.USER_PROFILE_IMAGE,
					ImageOwnerId = user.Id,
					MediaData = MediaMethods.ToByteArray(request.Base64Media),
				};

				ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueues.StateMachine_UserMedia}"));
				await sendEndpoint.Send<UserMediaCreatedEvent>(mediaCreatedEvent);
			}

			return await Commit(userRepository.UnitOfWork);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

		public async Task<FluentValidation.Results.ValidationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
