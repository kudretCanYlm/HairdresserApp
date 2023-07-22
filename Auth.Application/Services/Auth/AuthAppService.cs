using Auth.Application.Dto.Auth;
using Auth.Application.Interfaces.Auth;
using Auth.Domain.Commands.Auth;
using Auth.Domain.Interfaces.Auth;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;

namespace Auth.Application.Services.Auth
{
	public class AuthAppService : IAuthAppService
	{
		private readonly IAuthRepository _authRepository;
		private readonly IMapper _mapper;
		private readonly IMediatorHandler _mediatorNet;
		private readonly IMediator _mediator;

		public AuthAppService(IAuthRepository authRepository, IMapper mapper, IMediatorHandler mediatorNet, IMediator mediator)
		{
			_authRepository = authRepository;
			_mapper = mapper;
			_mediatorNet = mediatorNet;
			_mediator = mediator;
		}

		public async Task<UserAuthSessionDto> CheckTokenAndAddExpiring(string token)
		{
			var result= await _mediator.Send(new RefreshTokenCommand(token));

			return _mapper.Map<UserAuthSessionDto>(result);
		}

		public async Task<UserAuthSessionDto> CreateToken(Guid userId)
		{
			var result=await _mediator.Send(new LoginCommand(userId));
			return _mapper.Map<UserAuthSessionDto>(result);
		}

		public async Task<ValidationResult> DeleteToken(string token)
		{
			var result= await _mediatorNet.SendCommand(new LogoutCommand(token));

			return result;
		}

		public async Task<IEnumerable<UserAuthSessionDto>> GetAllTokens(Guid userId)
		{
			var result= await _authRepository.GetAllTokensAsync(userId);
			return _mapper.Map<IEnumerable<UserAuthSessionDto>>(result);
		}
	}
}
