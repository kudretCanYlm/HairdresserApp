using AutoMapper;
using Events.Stores;
using FluentValidation.Results;
using Hairdresser.Application.Dto;
using Hairdresser.Application.EventSourcedNormalizers.Hairdresser;
using Hairdresser.Application.Interfaces.Hairdresser;
using Hairdresser.Domain.Commands.Hairdresser;
using Hairdresser.Domain.Queries.Hairdresser;
using MediatR;
using NetDevPack.Mediator;

namespace Hairdresser.Application.Services.Hairdresser
{
	public class HairdresserAppService : IHairdresserAppService
	{
		private readonly IMapper _mapper;
		private readonly IEventStoreRepository _eventStoreRepository;
		private readonly IMediatorHandler _mediatorHandler;
		private readonly IMediator _mediator;

		public HairdresserAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler, IMediator mediator)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediatorHandler = mediatorHandler;
			_mediator = mediator;
		}

		public async Task<ValidationResult> CreateAsync(CreateHairdresserDto createHairdresserDto)
		{
			var command = _mapper.Map<CreateHairdresserCommand>(createHairdresserDto);
			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<IEnumerable<HairdresserDto>> GetAllHairdressers()
		{
			var result = await _mediator.Send(new GetAllHairdressersQuery());
			return _mapper.Map<IEnumerable<HairdresserDto>>(result);
		}

		public async Task<IList<HairdresserHistoryData>> GetAllHistoryAsync(Guid id)
		{
			return HairdresserHistory.ToJavaScriptAddressHistory(await _eventStoreRepository.All(id));
		}

		public async Task<HairdresserDto> GetHairdresserById(Guid id)
		{
			var result=await _mediator.Send(new GetHairdresserByIdQuery(id));
			return _mapper.Map<HairdresserDto>(result);
		}

		public async Task<ValidationResult> RemoveAsync(Guid id, Guid OwnerId)
		{
			var command=new DeleteHairdresserCommand(id, OwnerId);
			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> UpdateAsync(UpdateHairdresserDto updateHairdresserDto)
		{
			var command=_mapper.Map<UpdateHairdresserCommand>(updateHairdresserDto);
			return await _mediatorHandler.SendCommand(command);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
