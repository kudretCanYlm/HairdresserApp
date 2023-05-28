using AutoMapper;
using Common.Media;
using Events.Hairdresser;
using Events.MassTransitOptions;
using Hairdresser.Domain.Interfaces;
using Hairdresser.Domain.Models;
using MassTransit;
using MediatR;
using NetDevPack.Messaging;

namespace Hairdresser.Domain.Commands.Hairdresser
{
	public class HairdresserCommandHandler : CommandHandler,
											IRequestHandler<CreateHairdresserCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<DeleteHairdresserCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<UpdateHairdresserCommand, FluentValidation.Results.ValidationResult>
	{
		private readonly IHairdresserRepository _hairdresserRepository;
		private readonly IMapper _mapper;
		private readonly ISendEndpointProvider _sendEndpointProvider;

		public HairdresserCommandHandler(IHairdresserRepository hairdresserRepository, IMapper mapper, ISendEndpointProvider sendEndpointProvider)
		{
			_hairdresserRepository = hairdresserRepository;
			_mapper = mapper;
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(CreateHairdresserCommand request, CancellationToken cancellationToken)
		{
			var hairdresser = _mapper.Map<HairdresserModel>(request);
			_hairdresserRepository.Add(hairdresser);

			hairdresser.AddDomainEvent(_mapper.Map<HairdresserCreatedEvent>(hairdresser));

			if (request.Base64Media.Length != 0)
			{
				var mediaCreatedEvent = new HairdresserMediaCreatedEvent
				{
					Id = Guid.NewGuid(),
					FileExtension = MediaMethods.ToFileExtension(request.Base64Media),
					CustomType = MediaTypes.HAIRDRESSER_SINGLE,
					ImageOwnerId = hairdresser.Id,
					MediaData = MediaMethods.ToByteArray(request.Base64Media),
				};

				ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueues.StateMachine_HairdresserMedia}"));
				await sendEndpoint.Send<HairdresserMediaCreatedEvent>(mediaCreatedEvent);
			}


			return await Commit(_hairdresserRepository.UnitOfWork);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(DeleteHairdresserCommand request, CancellationToken cancellationToken)
		{
			var hairdresser = await _hairdresserRepository.GetByIdAndOwnerIdAsync(request.Id,request.OwnerId);

			if (hairdresser is null)
			{
				AddError("The hairdresser doesn't exists.");
				return ValidationResult;
			}

			hairdresser.AddDomainEvent(_mapper.Map<HairdresserDeletedEvent>(hairdresser));

			_hairdresserRepository.Delete(hairdresser);

			return await Commit(_hairdresserRepository.UnitOfWork);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(UpdateHairdresserCommand request, CancellationToken cancellationToken)
		{
			var hairdresser = await _hairdresserRepository.GetByIdAndOwnerIdAsync(request.Id, request.OwnerId);

			if (hairdresser == null)
			{
				AddError("hairdresser not found");
				return ValidationResult;
			}

			hairdresser = _mapper.Map(request, hairdresser);

			hairdresser.AddDomainEvent(_mapper.Map<HairdresserUpdatedEvent>(hairdresser));

			_hairdresserRepository.Update(hairdresser);

			return await Commit(_hairdresserRepository.UnitOfWork);
		}
	}
}
