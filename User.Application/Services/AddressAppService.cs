using AutoMapper;
using Events.Stores;
using FluentValidation.Results;
using NetDevPack.Mediator;
using User.Application.Dto.Address;
using User.Application.Dto.User;
using User.Application.EventSourcedNormalizers.Address;
using User.Application.Interfaces.Address;
using User.Domain.Commands.Address;
using User.Domain.Queries.Address;

namespace User.Application.Services
{
	public class AddressAppService : IAddressAppService
	{
		private readonly IMapper _mapper;
		private readonly IEventStoreRepository _eventStoreRepository;
		private readonly IMediatorHandler _mediator;

		public AddressAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediator = mediatorHandler;
		}

		public async Task<ValidationResult> Create(CreateAddressDto createUserAddressDto)
		{
			var command=_mapper.Map<CreateUserAddressCommand>(createUserAddressDto);
			return await _mediator.SendCommand(command);
		}

		public async Task<IEnumerable<AddressDto>> GetAll()
		{
			var command = new GetAllUserAddressesQuery();
			var result= await _mediator.SendCommand(command);
			return _mapper.Map<IEnumerable<AddressDto>>(result);
		}

		public async Task<IEnumerable<AddressDto>> GetAllByUserId(Guid userId)
		{
			var command = new GetUserAddressesByUserId(userId);
			var result = await _mediator.SendCommand(command);
			return _mapper.Map<IEnumerable<AddressDto>>(result);
		}

		public async Task<IList<AddressHistoryData>> GetAllHistory(Guid id)
		{
			return AddressHistory.ToJavaScriptAddressHistory(await _eventStoreRepository.All(id));
		}

		public async Task<UserDto> GetById(Guid id)
		{
			var command = new GetUserAddressByIdQuery(id);
			var result=await _mediator.SendCommand(command);
			return _mapper.Map<UserDto>(result);
		}

		public async Task<ValidationResult> Remove(Guid id)
		{
			var command = new DeleteUserAddressCommand(id);
			return await _mediator.SendCommand(command);
		}

		public async Task<ValidationResult> Update(UpdateAddressDto updateUserAddressDto)
		{
			var command = _mapper.Map<UpdateUserAddressCommand>(updateUserAddressDto);
			return await _mediator.SendCommand(command);
		}
	}
}
