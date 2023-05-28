using AutoMapper;
using Common.Media;
using Events.Stores;
using FluentValidation.Results;
using Grpc.Media.ClientServices;
using HairdresserService.Application.Dto;
using HairdresserService.Application.EventSourcedNormalizers.HairdresserService;
using HairdresserService.Application.Interfaces.HairdresserService;
using HairdresserService.Domain.Commands.HairdresserService;
using HairdresserService.Domain.Queries.HairdresserService;
using MediatR;
using NetDevPack.Mediator;

namespace HairdresserService.Application.Services
{
	public class HairdresserServiceAppService : IHairdresserServiceAppService
	{
		private readonly IMapper _mapper;
		private readonly IEventStoreRepository _eventStoreRepository;
		private readonly IMediatorHandler _mediatorHandler;
		private readonly IMediator _mediator;
		private readonly MediaGrpcService _mediaGrpcService;

		public HairdresserServiceAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler, IMediator mediator, MediaGrpcService mediaGrpcService)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediatorHandler = mediatorHandler;
			_mediator = mediator;
			_mediaGrpcService = mediaGrpcService;
		}

		public async Task<ValidationResult> ActivateHairdresserService(ActivateHairdresserServiceDto activateHairdresserServiceDto)
		{
			var command = _mapper.Map<ActivateHairdresserServiceCommand>(activateHairdresserServiceDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> CreateHairdresserService(CreateHairdresserServiceDto createHairdresserServiceDto)
		{
			var command = _mapper.Map<CreateHairdresserServiceCommand>(createHairdresserServiceDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> DeactivateHairdresserService(ActivateHairdresserServiceDto activateHairdresserServiceDto)
		{
			var command = _mapper.Map<DeactivateHairdresserServiceCommand>(activateHairdresserServiceDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> DeleteHairdresserService(DeleteHairdresserServiceDto deleteHairdresserServiceDto)
		{
			var command = _mapper.Map<DeleteHairdresserServiceCommand>(deleteHairdresserServiceDto);

			return await _mediatorHandler.SendCommand(command);
		}
		public async Task<ValidationResult> UpdateHairdresserService(UpdateHairdresserServiceDto updateHairdresserServiceDto)
		{
			var command = _mapper.Map<UpdateHairdresserServiceCommand>(updateHairdresserServiceDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<IEnumerable<HairdresserServiceDto>> GetAllHairdresserServicesByHairdresserId(Guid hairdresserId)
		{
			var result=await _mediator.Send(new GetAllHairdresserServicesByHairdresserIdQuery(hairdresserId));

			return _mapper.Map<IEnumerable<HairdresserServiceDto>>(result);
		}

		public async Task<HairdresserServiceImageDto> GetAllHairdresserServiceWithImagesById(Guid serviceId)
		{
			var result=await _mediator.Send(new GetHairdresserServiceByIdQuery(serviceId));
			var serviceWithImages = _mapper.Map<HairdresserServiceImageDto>(result);

			if (serviceWithImages == null)
				return serviceWithImages;

			var images = await _mediaGrpcService.GetAllMediasByOwnerIdAndTypeAsync(serviceId, MediaTypes.HAIRDRESSER_SERVICE_MULTI);

			serviceWithImages.Base64MediaList = images.Select(x=>x.Base64Media).ToList();

			return serviceWithImages;
		}

		public async Task<IEnumerable<HairdresserServiceHistoryData>> GetAllHistoryAsync(Guid id)
		{
			return HairdresserServiceHistory.ToJavaScriptHairdresserServiceHistory(await _eventStoreRepository.All(id));
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}


	}
}
