using AutoMapper;
using Common.Media;
using Database.PaggingAndFilter;
using Events.Stores;
using FluentValidation.Results;
using Grpc.Media.ClientServices;
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
		private readonly MediaGrpcService _mediaGrpcService;

		public HairdresserAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler, IMediator mediator, MediaGrpcService mediaGrpcService)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediatorHandler = mediatorHandler;
			_mediator = mediator;
			_mediaGrpcService = mediaGrpcService;
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
			var result = await _mediator.Send(new GetHairdresserByIdQuery(id));
			return _mapper.Map<HairdresserDto>(result);
		}

		public async Task<ValidationResult> RemoveAsync(Guid id, Guid OwnerId)
		{
			var command = new DeleteHairdresserCommand(id, OwnerId);
			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> UpdateAsync(UpdateHairdresserDto updateHairdresserDto)
		{
			var command = _mapper.Map<UpdateHairdresserCommand>(updateHairdresserDto);
			return await _mediatorHandler.SendCommand(command);
		}
		public async Task<IEnumerable<HairdresserImageDto>> GetAllHairdresserByFilter(PageSearchArgs pageSearchArgs)
		{
			var query = new GetAllHairdresserByFilterQuery(pageSearchArgs);

			var result = await _mediator.Send(query);

			var hairdressersWithImage = new List<HairdresserImageDto>();


			foreach (var item in result.Items)
			{
				var image = await _mediaGrpcService.GetMediaByOwnerIdAndTypeAsync(item.Id, MediaTypes.HAIRDRESSER_SINGLE);
				var hairdresserWithImage = _mapper.Map<HairdresserImageDto>(item);
				hairdresserWithImage.Base64Media = image.Base64Media;

				hairdressersWithImage.Add(hairdresserWithImage);
			}

			return hairdressersWithImage;
		}

		public async Task<HairdresserImageDto> GetHairdresserWithImageById(Guid id)
		{
			var hairdresser=await _mediator.Send(new GetHairdresserByIdQuery(id));
			var hairdresserWithImage = _mapper.Map<HairdresserImageDto>(hairdresser);

			var image = await _mediaGrpcService.GetMediaByOwnerIdAndTypeAsync(hairdresser.Id, MediaTypes.HAIRDRESSER_SINGLE);
			
			hairdresserWithImage.Base64Media= image.Base64Media;

			return hairdresserWithImage;
		}

		public async Task<bool> CheckHairdresserByIdAndUserId(Guid id, Guid userId)
		{
			var query=new CheckHairdresserIdAndUserIdQuery(id, userId);

			var result=await _mediator.Send(query);

			return result;
		}
		public async Task<bool> CheckHairdresserActive(Guid id, DateTime appointmentDate, TimeSpan appointmentStartTime, TimeSpan serviceDuration)
		{
			var query=new CheckHairdresserActiveQuery(id,appointmentDate,appointmentStartTime,serviceDuration);

			var result = await _mediator.Send(query);

			return result;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}


	}
}
