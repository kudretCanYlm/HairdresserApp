using AutoMapper;
using Events.Stores;
using Media.Application.Dto;
using Media.Application.EventSourcedNormalizers.Media;
using Media.Application.Interfaces.Media;
using Media.Domain.Commands.Media;
using NetDevPack.Mediator;
using FluentValidation.Results;
using Media.Domain.Queries.Media;
using MediatR;

namespace Media.Application.Services.Media
{
	public class MediaAppService : IMediaAppService
	{
		private readonly IMapper _mapper;
		private readonly IEventStoreRepository _eventStoreRepository;
		private readonly IMediatorHandler _mediatorHandler;
		private readonly IMediator _mediator;

		public MediaAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler, IMediator mediator)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediatorHandler = mediatorHandler;
			_mediator = mediator;
		}

		public async Task<ValidationResult> CreateAsync(CreateMediaDto createMediaDto)
		{
			var command = _mapper.Map<CreateMediaCommand>(createMediaDto);
			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<IEnumerable<MediaDto>> GetAllByOwnerIdAsync(Guid ownerId)
		{
			var result = await _mediator.Send(new GetAllMediasQuery(ownerId));
			return _mapper.Map<IEnumerable<MediaDto>>(result);
		}

		public async Task<MediaDto> GetByOwnerIdAndImageType(Guid ownerId, string type)
		{
			var result = await _mediator.Send(new GetMediaByImageOwnerIdAndTypeQuery(type, ownerId));
			return _mapper.Map<MediaDto>(result);
		}

		public async Task<IEnumerable<MediaDto>> GetMediaListByImageOwnerIdAndType(Guid ownerId, string type)
		{
			var result = await _mediator.Send(new GetMediaListByImageOwnerIdAndTypeQuery(type,ownerId));
			return _mapper.Map<IEnumerable<MediaDto>>(result);
		}

		public async Task<IList<MediaHistoryData>> GetAllHistoryAsync(Guid id)
		{
			return MediaHistory.ToJavaScriptMediaHistory(await _eventStoreRepository.All(id));
		}

		public async Task<ValidationResult> RemoveAsync(Guid id)
		{
			var command = new DeleteMediaCommand(id);
			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> UpdateAsync(UpdateMediaDto updateMediaDto)
		{
			var command = _mapper.Map<UpdateMediaCommand>(updateMediaDto);
			return await _mediatorHandler.SendCommand(command);
		}



		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}


	}
}
