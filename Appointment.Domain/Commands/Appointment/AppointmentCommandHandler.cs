using Appointment.Domain.Interfaces;
using Appointment.Domain.Models;
using AutoMapper;
using Events.Appointment;
using Events.MassTransitOptions;
using FluentValidation.Results;
using Grpc.Hairdresser.ClientServices;
using Grpc.HairdresserService.ClientServices;
using MassTransit;
using MediatR;
using NetDevPack.Messaging;

namespace Appointment.Domain.Commands.Appointment
{
	public class AppointmentCommandHandler : CommandHandler,
											IRequestHandler<ApproveAppointmentCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<CancelAppointmentCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<CompleteAppointmentCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<DenyAppointmentCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<InProcessAppointmentCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<UpdateAppointmentCommand, FluentValidation.Results.ValidationResult>,
											IRequestHandler<CreateAppointmentCommand, FluentValidation.Results.ValidationResult>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IMapper _mapper;
		private readonly HairdresserServiceGrpcService _hairdresserServiceGrpcService;
		private readonly HairdresserGrpcService _hairdresserGrpcService;
		private readonly ISendEndpointProvider _sendEndpointProvider;

		public AppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper, HairdresserServiceGrpcService hairdresserServiceGrpcService, HairdresserGrpcService hairdresserGrpcService, ISendEndpointProvider sendEndpointProvider)
		{
			_appointmentRepository = appointmentRepository;
			_mapper = mapper;
			_hairdresserServiceGrpcService = hairdresserServiceGrpcService;
			_hairdresserGrpcService = hairdresserGrpcService;
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentApprovedEvent>(request, false);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentCanceledEvent>(request, true);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentCompletedEvent>(request, true);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(DenyAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentDeniedEvent>(request, true);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(InProcessAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentInProcessedEvent>(request, true);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
		{
			return await UpdateByHairdresserByUserIdOrHairdresserId<AppointmentUpdatedEvent>(request, true);
		}

		public async Task<FluentValidation.Results.ValidationResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
		{
			var inThisWeekAndGreaterThanNow =
				DateOnly.FromDateTime(request.AppointmentDate) >= DateOnly.FromDateTime(DateTime.Now)
				&&
				DateOnly.FromDateTime(request.AppointmentDate) <= DateOnly.FromDateTime(DateTime.Now).AddDays(7);

			if (!inThisWeekAndGreaterThanNow)
			{
				AddError("Invalid Time");
				return ValidationResult;
			}

			var hairdresserService = await _hairdresserServiceGrpcService.GetHairdresserService(request.HairdresserServiceId, request.HairdresserId);

			if (!hairdresserService.IsAny)
			{
				AddError("Service Did't find");
				return ValidationResult;
			}

			bool isHairdresserActive = await _hairdresserGrpcService.CheckHairdresserActive(request.HairdresserId, request.AppointmentDate, request.AppointmentStartTime, TimeSpan.Parse(hairdresserService.ServiceDuration));

			if (!isHairdresserActive)
			{
				AddError("Hairdresser Is Not Active In This Time");
				return ValidationResult;
			}

			bool isthereAnAppointment = await _appointmentRepository.CheckIsthereAnAppointment(request.HairdresserId, request.AppointmentDate, request.AppointmentStartTime, TimeSpan.Parse(hairdresserService.ServiceDuration));

			if (isthereAnAppointment)
			{
				AddError("There is an appointment this time");
				return ValidationResult;
			}

			request.AppointmentEndTime = request.AppointmentStartTime.Add(TimeSpan.Parse(hairdresserService.ServiceDuration));

			var appointment = _mapper.Map<AppointmentModel>(request);

			_appointmentRepository.Add(appointment);
			var appointmentCreatedEvent = _mapper.Map<AppointmentCreatedEvent>(appointment);

			appointment.AddDomainEvent(appointmentCreatedEvent);

			ISendEndpoint _applicationEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueues.Appointment_AppointmentCreatedEventQueue}"));

			_applicationEndpoint.Send(appointmentCreatedEvent);

			return await Commit(_appointmentRepository.UnitOfWork);

		}

		private async Task<FluentValidation.Results.ValidationResult> UpdateByHairdresserByUserIdOrHairdresserId<T>(AppointmentCommand request, bool isByUserId) where T : NetDevPack.Messaging.Event
		{
			var appointment = isByUserId ? await _appointmentRepository.GetAppointmentByIdAndUserId(request.Id, request.UserId) : await _appointmentRepository.GetAppointmentByIdAndHairdresserId(request.Id, request.HairdresserId);

			if (appointment == null)
			{
				AddError("Appointment Not Found");
				return ValidationResult;
			}

			//not work in update
			appointment.AppointmentState = request.AppointmentState;

			var appointmentEvent = _mapper.Map<T>(appointment);

			appointment.AddDomainEvent(appointmentEvent);

			_appointmentRepository.Update(appointment);

			switch (request.GetType().Name)
			{
				case nameof(ApproveAppointmentCommand):
					ISendEndpoint _applicationEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueues.Appointment_AppointmentApprovedEventQueue}"));
					_applicationEndpoint.Send(appointmentEvent);

					break;

				default:
					break;
			}

			return await Commit(_appointmentRepository.UnitOfWork);
		}
	}
}
