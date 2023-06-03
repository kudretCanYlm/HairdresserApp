using Grpc.HairdresserService.Protos;

namespace Grpc.HairdresserService.ClientServices
{
	public class HairdresserServiceGrpcService
	{
		private readonly HairdresserServiceProtoService.HairdresserServiceProtoServiceClient _hairdresserServiceProtoServiceClient;

		public HairdresserServiceGrpcService(HairdresserServiceProtoService.HairdresserServiceProtoServiceClient hairdresserServiceProtoServiceClient)
		{
			_hairdresserServiceProtoServiceClient = hairdresserServiceProtoServiceClient;
		}

		public async Task<HairdresserServiceModel> GetHairdresserService(Guid serviceId, Guid hairdresserId)
		{
			var request = new GetHairdresserServiceRequest
			{
				ServiceId=serviceId.ToString(),
				HairdresserId=hairdresserId.ToString(),
			};

			var result = await _hairdresserServiceProtoServiceClient.GetHairdresserServiceAsync(request);

			return result;
		}

		public async Task<string> GetHairdresserServiceNameById(Guid serviceId, Guid hairdresserId)
		{
			var request = new GetHairdresserServiceRequest
			{
				ServiceId = serviceId.ToString(),
				HairdresserId = hairdresserId.ToString(),
			};

			var result = await _hairdresserServiceProtoServiceClient.GetHairdresserServiceNameByIdAsync(request);

			return result.ServiceName;
		}
	}
}
