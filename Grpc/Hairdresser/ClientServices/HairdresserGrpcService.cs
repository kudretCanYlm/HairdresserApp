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
	}
}
