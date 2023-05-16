using AutoMapper;
using Events.Hairdresser;
using Events.Media;
using FluentValidation.Results;
using Hairdresser.Domain.Interfaces;
using Hairdresser.Domain.Models;
using MediatR;
using NetDevPack.Messaging;
using static StackExchange.Redis.Role;

namespace Hairdresser.Domain.Commands.Hairdresser
{
	public class HairdresserCommandHandler : CommandHandler,
											IRequestHandler<CreateHairdresserCommand, ValidationResult>,
											IRequestHandler<DeleteHairdresserCommand, ValidationResult>,
											IRequestHandler<UpdateHairdresserCommand, ValidationResult>
	{
		private readonly IHairdresserRepository _hairdresserRepository;
		private readonly IMapper _mapper;

		public HairdresserCommandHandler(IHairdresserRepository hairdresserRepository, IMapper mapper)
		{
			_hairdresserRepository = hairdresserRepository;
			_mapper = mapper;
		}

		public async Task<ValidationResult> Handle(CreateHairdresserCommand request, CancellationToken cancellationToken)
		{
			var hairdresser = _mapper.Map<HairdresserModel>(request);
			_hairdresserRepository.Add(hairdresser);

			hairdresser.AddDomainEvent(_mapper.Map<HairdresserCreatedEvent>(hairdresser));

			return await Commit(_hairdresserRepository.UnitOfWork);
		}

		public async Task<ValidationResult> Handle(DeleteHairdresserCommand request, CancellationToken cancellationToken)
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

		public async Task<ValidationResult> Handle(UpdateHairdresserCommand request, CancellationToken cancellationToken)
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
