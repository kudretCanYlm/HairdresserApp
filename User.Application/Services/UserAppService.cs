using AutoMapper;
using Events.Stores;
using FluentValidation.Results;
using NetDevPack.Mediator;
using User.Application.EventSourcedNormalizers.User;
using User.Application.Interfaces.User;
using User.Domain.Commands.User;
using User.Application.Dto.User;
using User.Domain.Queries.User;

namespace User.Application.Services
{
	public class UserAppService : IUserAppService
	{
		private readonly IMapper _mapper;
		private readonly IEventStoreRepository _eventStoreRepository;
		private readonly IMediatorHandler _mediator;

		public UserAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediator)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediator = mediator;
		}

		public async Task<ValidationResult> CreateAsync(CreateUserDto createUserDto)
		{
			var createUserCommand = _mapper.Map<CreateUserCommand>(createUserDto);
			return await _mediator.SendCommand(createUserCommand);
		}

		public async Task<IEnumerable<UserDto>> GetAllAsync()
		{
			var users = await _mediator.SendCommand(new GetAllUsersQuery());
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<IList<UserHistoryData>> GetAllHistoryAsync(Guid id)
		{
			return UserHistory.ToJavaScriptUserHistory(await _eventStoreRepository.All(id));
		}

		public async Task<UserDto> GetByIdAsync(Guid id)
		{
			var user = await _mediator.SendCommand(new GetUserByIdQuery(id));
			return _mapper.Map<UserDto>(user);
		}

		public async Task<ValidationResult> RemoveAsync(Guid id)
		{
			var deleteUserCommand=new DeleteUserCommand(id);
			return await _mediator.SendCommand(deleteUserCommand);
		}

		public async Task<ValidationResult> UpdateAsync(UpdateUserDto updateUserDto)
		{
			var updateUserCommand=_mapper.Map<UpdateUserCommand>(updateUserDto);
			return await _mediator.SendCommand(updateUserCommand);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
