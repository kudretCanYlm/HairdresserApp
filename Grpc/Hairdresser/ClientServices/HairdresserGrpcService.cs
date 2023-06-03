using Grpc.Hairdresser.Protos;

namespace Grpc.Hairdresser.ClientServices
{
	public class HairdresserGrpcService
	{
		private readonly HairdresserProtoService.HairdresserProtoServiceClient _hairdresserProtoService;

		public HairdresserGrpcService(HairdresserProtoService.HairdresserProtoServiceClient hairdresserProtoService)
		{
			_hairdresserProtoService = hairdresserProtoService;
		}

		public async Task<bool> CheckHairdresserIdAndUserId(Guid hairdresserId,Guid userId)
		{
			var request = new CheckHairdresserIdAndUserIdRequest
			{
				HairdresserId = hairdresserId.ToString(),
				UserId = userId.ToString()
			};

			var status=await _hairdresserProtoService.CheckHairdresserIdAndUserIdAsync(request);

			return status.IsThere;
		}

		public async Task<bool> CheckHairdresserActive(Guid id, DateTime appointmentDate, TimeSpan appointmentStartTime, TimeSpan serviceDuration)
		{
			var request = new CheckHairdresserActiveRequest
			{
				HairdresserId = id.ToString(),
				AppointmentDate = appointmentDate.ToString(),
				AppointmentStartTime = appointmentStartTime.ToString(),
				ServiceDuration = serviceDuration.ToString(),
			};

			var result = await _hairdresserProtoService.CheckHairdresserActiveAsync(request);

			return result.IsActive;

		}

		public async Task<Guid> GetHairdresserOwnerId(Guid hairdresserId)
		{
			var request = new GetHairdresserOwnerIdRequest
			{
				HairdresserId = hairdresserId.ToString()
			};

			var result = await _hairdresserProtoService.GetHairdresserOwnerIdAsync(request);

			return Guid.Parse(result.OwnerId);
		}
	}
}
