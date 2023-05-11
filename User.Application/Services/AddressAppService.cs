using AutoMapper;
using Events.Stores;
using FluentValidation.Results;
using MediatR;
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
		private readonly IMediatorHandler _mediatorHandler;
		private readonly IMediator _mediator;

		public AddressAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler, IMediator mediator)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediatorHandler = mediatorHandler;
			_mediator = mediator;
		}

		public async Task<ValidationResult> Create(CreateAddressDto createUserAddressDto)
		{
			var query=_mapper.Map<CreateUserAddressCommand>(createUserAddressDto);
			return await _mediatorHandler.SendCommand(query);
		}

		public async Task<IEnumerable<AddressDto>> GetAll()
		{
			var query = new GetAllUserAddressesQuery();
			var result= await _mediator.Send(query);
			return _mapper.Map<IEnumerable<AddressDto>>(result);
		}

		public async Task<IEnumerable<AddressDto>> GetAllByUserId(Guid userId)
		{
			var query = new GetUserAddressesByUserId(userId);
			var result = await _mediator.Send(query);
			return _mapper.Map<IEnumerable<AddressDto>>(result);
		}

		public async Task<IList<AddressHistoryData>> GetAllHistory(Guid id)
		{
			return AddressHistory.ToJavaScriptAddressHistory(await _eventStoreRepository.All(id));
		}

		public async Task<UserDto> GetById(Guid id)
		{
			var command = new GetUserAddressByIdQuery(id);
			var result=await _mediator.Send(command);
			return _mapper.Map<UserDto>(result);
		}

		public async Task<ValidationResult> Remove(Guid id)
		{
			var command = new DeleteUserAddressCommand(id);
			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> Update(UpdateAddressDto updateUserAddressDto)
		{
			var command = _mapper.Map<UpdateUserAddressCommand>(updateUserAddressDto);
			return await _mediatorHandler.SendCommand(command);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
