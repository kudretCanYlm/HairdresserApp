using AutoMapper;
using Events.Stores;
using FluentValidation.Results;
using NetDevPack.Mediator;
using User.Application.EventSourcedNormalizers.User;
using User.Application.Interfaces.User;
using User.Domain.Commands.User;
using User.Application.Dto.User;
using User.Domain.Queries.User;
using MediatR;
using Grpc.Media.ClientServices;
using Common.Media;

namespace User.Application.Services
{
	public class UserAppService : IUserAppService
	{
		private readonly IMapper _mapper;
		private readonly IEventStoreRepository _eventStoreRepository;
		private readonly IMediatorHandler _mediatorHandler;
		private readonly IMediator _mediator;
		private readonly MediaGrpcService _mediaGrpcService;

		public UserAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler, IMediator mediator, MediaGrpcService mediaGrpcService)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediatorHandler = mediatorHandler;
			_mediator = mediator;
			_mediaGrpcService = mediaGrpcService;
		}

		public async Task<Guid?> Login(LoginDto loginDto)
		{
			var loginQuery = _mapper.Map<UserLoginQuery>(loginDto);
			var userId = await _mediator.Send(loginQuery);
			
			return userId;
		}

		public async Task<ValidationResult> CreateAsync(CreateUserDto createUserDto)
		{
			var createUserCommand = _mapper.Map<CreateUserCommand>(createUserDto);
			return await _mediatorHandler.SendCommand(createUserCommand);
		}

		public async Task<IEnumerable<UserDto>> GetAllAsync()
		{
			var users = await _mediator.Send(new GetAllUsersQuery());
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<IList<UserHistoryData>> GetAllHistoryAsync(Guid id)
		{
			return UserHistory.ToJavaScriptUserHistory(await _eventStoreRepository.All(id));
		}

		public async Task<UserDto> GetByIdAsync(Guid id)
		{
			var user = await _mediator.Send(new GetUserByIdQuery(id));

			var userdto = _mapper.Map<UserDto>(user);

			if(userdto != null)
			{
				var media = await _mediaGrpcService.GetMediaByOwnerIdAndTypeAsync(user.Id, MediaTypes.USER_PROFILE_IMAGE);

				userdto.MediaId = Guid.Parse(media.Id);
				userdto.Base64Media = media.Base64Media;
			}

			return userdto;
		}

		public async Task<ValidationResult> RemoveAsync(Guid id)
		{
			var deleteUserCommand=new DeleteUserCommand(id);
			return await _mediatorHandler.SendCommand(deleteUserCommand);
		}

		public async Task<ValidationResult> UpdateAsync(UpdateUserDto updateUserDto)
		{
			var updateUserCommand=_mapper.Map<UpdateUserCommand>(updateUserDto);
			return await _mediatorHandler.SendCommand(updateUserCommand);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}


	}
}
