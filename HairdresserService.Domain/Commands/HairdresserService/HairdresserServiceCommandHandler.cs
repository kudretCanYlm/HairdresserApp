using AutoMapper;
using Common.Media;
using Events.HairdresserService;
using Events.MassTransitOptions;
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

		public HairdresserServiceCommandHandler(IHairdresserServiceRepository hairdresserServiceRepository, IMapper mapper, ISendEndpointProvider sendEndpointProvider)
		{
			_hairdresserServiceRepository = hairdresserServiceRepository;
			_mapper = mapper;
			_sendEndpointProvider = sendEndpointProvider;
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
			var hairdresserService = await _hairdresserServiceRepository.GetByIdAndHairdresserId(request.Id, request.HairdresserId);

			if (hairdresserService == null)
			{
				AddError("Service Not Found");
				return ValidationResult;
			}

			hairdresserService=_mapper.Map(request,hairdresserService);

			hairdresserService.AddDomainEvent(_mapper.Map<HairdresserServiceUpdatedEvent>(hairdresserService));

			_hairdresserServiceRepository.Update(hairdresserService);

			return await Commit(_hairdresserServiceRepository.UnitOfWork);

		}
	}
}
