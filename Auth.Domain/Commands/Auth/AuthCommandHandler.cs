using Auth.Domain.Interfaces.Auth;
using Auth.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace Auth.Domain.Commands.Auth
{
	public class AuthCommandHandler : CommandHandler, IRequestHandler<LoginCommand, AuthSessionModel>,
												   IRequestHandler<LogoutCommand, ValidationResult>,
												   IRequestHandler<RefreshTokenCommand, AuthSessionModel>
	{
		private readonly IAuthRepository _authRepository;

		public AuthCommandHandler(IAuthRepository authRepository)
		{
			this._authRepository = authRepository;
		}

		public async Task<AuthSessionModel> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var result=await _authRepository.CreateTokenAsync(request.TokenOwnerId);
			return result;
	
		}

		public async Task<ValidationResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
		{
			var result= await _authRepository.DeleteTokenAsync(request.Token);

			if (!result)
				AddError("Token Not Found");

			return ValidationResult;
		}

		public async Task<AuthSessionModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var result=await _authRepository.RefreshTokenAsync(request.Token);
			return result;
		}
	}
}
