using Appointment.Application.Dto;
using Appointment.Application.EventSourcedNormalizers.Appointment;
using Appointment.Application.Interfaces.Appointment;
using Appointment.Domain.Commands.Appointment;
using Appointment.Domain.Queries.Appointment;
using AutoMapper;
using Events.Stores;
using FluentValidation.Results;
using Grpc.Hairdresser.ClientServices;
using MediatR;
using NetDevPack.Mediator;

namespace Appointment.Application.Services
{
	public class AppointmentAppService : IAppointmentAppService
	{
		private readonly IMapper _mapper;
		private readonly IEventStoreRepository _eventStoreRepository;
		private readonly IMediatorHandler _mediatorHandler;
		private readonly IMediator _mediator;
		private readonly HairdresserGrpcService _hairdresserGrpcService;

		public AppointmentAppService(IMapper mapper, IEventStoreRepository eventStoreRepository, IMediatorHandler mediatorHandler, IMediator mediator, HairdresserGrpcService hairdresserGrpcService)
		{
			_mapper = mapper;
			_eventStoreRepository = eventStoreRepository;
			_mediatorHandler = mediatorHandler;
			_mediator = mediator;
			_hairdresserGrpcService = hairdresserGrpcService;
		}

		public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsByUserId(Guid userId)
		{
			var result=await _mediator.Send(new GetAllAppointmentsByUserIdQuery(userId));

			return _mapper.Map<IEnumerable<AppointmentDto>>(result);
		}

		public async Task<AppointmentDto> GetAppointmentByIdAndUserId(Guid id, Guid userId)
		{
			var result = await _mediator.Send(new GetAppointmentByIdAndUserIdQuery(id,userId));

			return _mapper.Map<AppointmentDto>(result);

		}

		public async Task<AppointmentDto> GetAppointmentById(Guid id)
		{
			var result=await _mediator.Send(new GetAppointmentByIdQuery(id));

			return _mapper.Map<AppointmentDto>(result);
		}

		public async Task<ValidationResult> ApproveAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto)
		{
			var command = _mapper.Map<ApproveAppointmentCommand>(appointmentStateHairdresserDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> CancelAppointment(AppointmentStateUserDto appointmentStateUserDto)
		{
			var command = _mapper.Map<CancelAppointmentCommand>(appointmentStateUserDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> CompleteAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto)
		{
			var command = _mapper.Map<CompleteAppointmentCommand>(appointmentStateHairdresserDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> CreateAppointment(CreateAppointmentDto createAppointmentDto)
		{
			var command = _mapper.Map<CreateAppointmentCommand>(createAppointmentDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> DenyAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto)
		{
			var command = _mapper.Map<DenyAppointmentCommand>(appointmentStateHairdresserDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> InProcessAppointment(AppointmentStateHairdresserDto appointmentStateHairdresserDto)
		{
			var command = _mapper.Map<InProcessAppointmentCommand>(appointmentStateHairdresserDto);

			return await _mediatorHandler.SendCommand(command);
		}

		public async Task<ValidationResult> UpdateAppointment(UpdateAppointmentDto updateAppointmentDto)
		{
			var command = _mapper.Map<UpdateAppointmentCommand>(updateAppointmentDto);

			return await _mediatorHandler.SendCommand(command);
		}
		public async Task<IList<AppointmentHistoryData>> GetAllHistoryAsync(Guid id)
		{
			return AppointmentHistory.ToJavaScriptAppointmentHistory(await _eventStoreRepository.All(id));
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<IEnumerable<GetAllAppointmentsForUserDto>> GetAllAppointmentsForUser(GetAllAppointmentsForUserPostDto getAllAppointmentsForUserPostDto)
		{
			var appointments=await _mediator.Send(_mapper.Map<GetAllAppointmentsForUserQuery>(getAllAppointmentsForUserPostDto));

			return _mapper.Map<IEnumerable<GetAllAppointmentsForUserDto>>(appointments);

	}
	}
}
