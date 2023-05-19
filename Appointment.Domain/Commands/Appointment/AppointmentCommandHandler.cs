using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using AutoMapper;
using Events.Appointment;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace Appointment.Domain.Commands.Appointment
{
	public class AppointmentCommandHandler : CommandHandler,
											IRequestHandler<ApproveAppointmentCommand, ValidationResult>,
											IRequestHandler<CancelAppointmentCommand, ValidationResult>,
											IRequestHandler<CompleteAppointmentCommand, ValidationResult>,
											IRequestHandler<DenyAppointmentCommand, ValidationResult>,
											IRequestHandler<InProcessAppointmentCommand, ValidationResult>,
											IRequestHandler<UpdateAppointmentCommand, ValidationResult>,
											IRequestHandler<CreateAppointmentCommand,ValidationResult>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IMapper _mapper;

		public AppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
		{
			_appointmentRepository = appointmentRepository;
			_mapper = mapper;
		}

		public async Task<ValidationResult> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentApprovedEvent>(request, false);
		}

		public async Task<ValidationResult> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentCanceledEvent>(request, true);
		}

		public async Task<ValidationResult> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentCompletedEvent>(request, true);
		}

		public async Task<ValidationResult> Handle(DenyAppointmentCommand request, CancellationToken cancellationToken)
		{
			return  await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentDeniedEvent>(request, true);
		}

		public async Task<ValidationResult> Handle(InProcessAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentInProcessedEvent>(request, true);
		}

		public async Task<ValidationResult> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentUpdatedEvent>(request, true);
		}

		public async Task<ValidationResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
		{
			var appointment = _mapper.Map<AppointmentModel>(request);

			_appointmentRepository.Add(appointment);

			appointment.AddDomainEvent(_mapper.Map<AppointmentCreatedEvent>(appointment));

			return await Commit(_appointmentRepository.UnitOfWork);

		}

		private async Task<ValidationResult> UpdateByHairdresserByUserIdOrHairdresserId<T>(AppointmentCommand request,bool isByUserId) where T:Event
		{
			var appointment = isByUserId ? await _appointmentRepository.GetAppointmentByIdAndUserId(request.Id, request.UserId) :  await _appointmentRepository.GetAppointmentByIdAndHairdresserId(request.Id, request.HairdresserId);

			if (appointment == null)
			{
				AddError("Appointment Not Found");
				return ValidationResult;
			}

			appointment = _mapper.Map(request, appointment);

			appointment.AddDomainEvent(_mapper.Map<T>(appointment));

			_appointmentRepository.Update(appointment);

			return await Commit(_appointmentRepository.UnitOfWork);
		}
	}
}
