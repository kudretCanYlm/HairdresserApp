using AutoMapper;
using Common.Media;
using Events.HairdresserService;
using Events.MassTransitOptions;
using Grpc.Media.ClientServices;
using HairdresserService.Domain.Interfaces;
using HairdresserService.Domain.Models;
using MassTransit;
using MediatR;
using NetDevPack.Messaging;

namespace HairdresserService.Domain.Commands.HairdresserService
{
	public class HairdresserServiceCommandHandler : CommandHandler,
													IRequestHandler<ActivateHairdresserServiceCommand, FluentValidation.Results.ValidationResult>,
													IRequestHandler<CreateHairdresserServiceCommand, FluentValidation.Results.ValidationResult>,
													IRequestHandler<DeactivateHairdresserServiceCommand, FluentValidation.Results.ValidationResult>,
													IRequestHandler<DeleteHairdresserServiceCommand, FluentValidation.Results.ValidationResult>,
													IRequestHandler<UpdateHairdresserServiceCommand, FluentValidation.Results.ValidationResult>
	{
		private readonly IHairdresserServiceRepository _hairdresserServiceRepository;
		private readonly IMapper _mapper;
		private readonly ISendEndpointProvider _sendEndpointProvider;
		private readonly MediaGrpcService _mediaGrpcService;

		public HairdresserServiceCommandHandler(IHairdresserServiceRepository hairdresserServiceRepository, IMapper mapper, ISendEndpointProvider sendEndpointProvider, MediaGrpcService mediaGrpcService)
		{
			_hairdresserServiceRepository = hairdresserServiceRepository;
			_mapper = mapper;
			_sendEndpointProvider = sendEndpointProvider;
			_mediaGrpcService = mediaGrpcService;
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(ActivateHairdresserServiceCommand request, CancellationToken cancellationToken)
		{
			var hairdresserService = await _hairdresserServiceRepository.GetByIdAndHairdresserId(request.Id, request.HairdresserId);

			if (hairdresserService == null)
			{
				AddError("Service Not Found");
				return ValidationResult;
			}

			hairdresserService = _mapper.Map(request, hairdresserService);


			_hairdresserServiceRepository.Update(hairdresserService);

			hairdresserService.AddDomainEvent(_mapper.Map<HairdresserServiceActivatedEvent>(hairdresserService));


			return await Commit(_hairdresserServiceRepository.UnitOfWork);

		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(CreateHairdresserServiceCommand request, CancellationToken cancellationToken)
		{
			var hairdresserService = _mapper.Map<HairdresserServiceModel>(request);

			_hairdresserServiceRepository.Add(hairdresserService);

			hairdresserService.AddDomainEvent(_mapper.Map<HairdresserServiceCreatedEvent>(hairdresserService));

			ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueues.StateMachine_HairdresserServiceMedia}"));

			foreach (var base64Media in request.Base64MediaList)
			{
				var mediaCreatedEvent = new HairdresserServiceMediaCreatedEvent
				{
					Id = Guid.NewGuid(),
					FileExtension = MediaMethods.ToFileExtension(base64Media),
					CustomType = MediaTypes.HAIRDRESSER_SERVICE_MULTI,
					ImageOwnerId = hairdresserService.Id,
					MediaData = MediaMethods.ToByteArray(base64Media),
				};

				await sendEndpoint.Send<HairdresserServiceMediaCreatedEvent>(mediaCreatedEvent);

			}

			return await Commit(_hairdresserServiceRepository.UnitOfWork);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(DeactivateHairdresserServiceCommand request, CancellationToken cancellationToken)
		{
			var hairdresserService = await _hairdresserServiceRepository.GetByIdAndHairdresserId(request.Id, request.HairdresserId);

			if (hairdresserService == null)
			{
				AddError("Service Not Found");
				return ValidationResult;
			}

			hairdresserService = _mapper.Map(request, hairdresserService);


			_hairdresserServiceRepository.Update(hairdresserService);

			hairdresserService.AddDomainEvent(_mapper.Map<HairdresserServiceDeactivatedEvent>(hairdresserService));


			return await Commit(_hairdresserServiceRepository.UnitOfWork);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(DeleteHairdresserServiceCommand request, CancellationToken cancellationToken)
		{
			var hairdresserService = await _hairdresserServiceRepository.GetByIdAndHairdresserId(request.Id, request.HairdresserId);

			if (hairdresserService == null)
			{
				AddError("Service Not Found");
				return ValidationResult;
			}

			hairdresserService.AddDomainEvent(_mapper.Map<HairdresserServiceDeletedEvent>(hairdresserService));

			_hairdresserServiceRepository.Delete(hairdresserService);

			return await Commit(_hairdresserServiceRepository.UnitOfWork);

		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(UpdateHairdresserServiceCommand request, CancellationToken cancellationToken)
		{
			foreach (var image in request.Medias.Where(x => x.Operation != OperationEnum.Create))
			{
				var status = await _mediaGrpcService.IsMediaAvailable(image.Id,request.Id);

				if (!status)
				{
					AddError("Deleted Media Not Found");
					return ValidationResult;
				}
			}

			var count = 5 - await _mediaGrpcService.GetMediaCount(request.Id, MediaTypes.HAIRDRESSER_SERVICE_MULTI);

			if (!MediaUpdateModel.SetSizeValidatinon(request.Medias, count))
			{
				AddError("Too Many Media");
				return ValidationResult;
			}

			var hairdresserService = await _hairdresserServiceRepository.GetByIdAndHairdresserId(request.Id, request.HairdresserId);

			if (hairdresserService == null)
			{
				AddError("Service Not Found");
				return ValidationResult;
			}

			hairdresserService = _mapper.Map(request, hairdresserService);

			hairdresserService.AddDomainEvent(_mapper.Map<HairdresserServiceUpdatedEvent>(hairdresserService));

			_hairdresserServiceRepository.Update(hairdresserService);

			ISendEndpoint _mediaEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueues.StateMachine_HairdresserServiceMedia}"));

			foreach (var media in request.Medias)
			{
				switch (media.Operation)
				{
					case OperationEnum.Delete:

						var hairdresserServiceMediaDeletedEvent = new HairdresserServiceMediaDeletedEvent
						{
							Id = media.Id,
							ImageOwnerId = request.Id
						};

						_mediaEndpoint.Send<HairdresserServiceMediaDeletedEvent>(hairdresserServiceMediaDeletedEvent);

						break;
					case OperationEnum.Update:

						var hairdresserServiceMediaUpdatedEvent = new HairdresserServiceMediaUpdatedEvent
						{
							Id = media.Id,
							ImageOwnerId = request.Id,
							FileExtension = MediaMethods.ToFileExtension(media.Base64Image),
							CustomType = MediaTypes.HAIRDRESSER_SERVICE_MULTI,
							MediaData = MediaMethods.ToByteArray(media.Base64Image),
						};

						_mediaEndpoint.Send<HairdresserServiceMediaUpdatedEvent>(hairdresserServiceMediaUpdatedEvent);
						
						break;
					case OperationEnum.Create:

						var hairdresserServiceMediaCreatedEvent = new HairdresserServiceMediaCreatedEvent
						{
							Id = Guid.NewGuid(),
							ImageOwnerId = request.Id,
							FileExtension = MediaMethods.ToFileExtension(media.Base64Image),
							CustomType = MediaTypes.HAIRDRESSER_SERVICE_MULTI,
							MediaData = MediaMethods.ToByteArray(media.Base64Image),
						};

						_mediaEndpoint.Send<HairdresserServiceMediaCreatedEvent>(hairdresserServiceMediaCreatedEvent);

						break;
					default:
						break;
				}
			}

			return await Commit(_hairdresserServiceRepository.UnitOfWork);

		}
	}
}
