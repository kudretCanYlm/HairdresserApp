using Grpc.Core;
using Grpc.Hairdresser.Protos;
using Hairdresser.Application.Interfaces.Hairdresser;

namespace Hairdresser.GRPC.Services
{
	public class HairdresserService:HairdresserProtoService.HairdresserProtoServiceBase
	{
		private readonly IHairdresserAppService _hairdresserAppService;

		public HairdresserService(IHairdresserAppService hairdresserAppService)
		{
			_hairdresserAppService = hairdresserAppService;
		}

		public override async Task<CheckHairdresserModel> CheckHairdresserIdAndUserId(CheckHairdresserIdAndUserIdRequest request, ServerCallContext context)
		{
			var check = await _hairdresserAppService.CheckHairdresserByIdAndUserId(Guid.Parse(request.HairdresserId),Guid.Parse(request.UserId));

			var result = new CheckHairdresserModel
			{
				IsThere = check
			};

			return result;
		}
	}
}
