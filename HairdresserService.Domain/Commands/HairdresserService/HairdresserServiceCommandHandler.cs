using AutoMapper;
using Events.HairdresserService;
using FluentValidation.Results;
using HairdresserService.Domain.Interfaces;
using HairdresserService.Domain.Models;
using MediatR;
using NetDevPack.Messaging;

namespace HairdresserService.Domain.Commands.HairdresserService
{
	public class HairdresserServiceCommandHandler : CommandHandler,
													IRequestHandler<ActivateHairdresserServiceCommand, ValidationResult>,
													IRequestHandler<CreateHairdresserServiceCommand, ValidationResult>,
													IRequestHandler<DeactivateHairdresserServiceCommand, ValidationResult>,
													IRequestHandler<DeleteHairdresserServiceCommand, ValidationResult>,
													IRequestHandler<UpdateHairdresserServiceCommand, ValidationResult>
	{
		private readonly IHairdresserServiceRepository _hairdresserServiceRepository;
		private readonly IMapper _mapper;

		public HairdresserServiceCommandHandler(IHairdresserServiceRepository hairdresserServiceRepository, IMapper mapper)
		{
			_hairdresserServiceRepository = hairdresserServiceRepository;
			_mapper = mapper;
		}

		public async Task<ValidationResult> Handle(ActivateHairdresserServiceCommand request, CancellationToken cancellationToken)
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

		public async Task<ValidationResult> Handle(CreateHairdresserServiceCommand request, CancellationToken cancellationToken)
		{
			var hairdresserService = _mapper.Map<HairdresserServiceModel>(request);

			_hairdresserServiceRepository.Add(hairdresserService);

			hairdresserService.AddDomainEvent(_mapper.Map<HairdresserServiceCreatedEvent>(hairdresserService));

			return await Commit(_hairdresserServiceRepository.UnitOfWork);
		}

		public async Task<ValidationResult> Handle(DeactivateHairdresserServiceCommand request, CancellationToken cancellationToken)
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

		public async Task<ValidationResult> Handle(DeleteHairdresserServiceCommand request, CancellationToken cancellationToken)
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

		public async Task<ValidationResult> Handle(UpdateHairdresserServiceCommand request, CancellationToken cancellationToken)
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
