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

		public override async Task<CheckHairdresserActiveModel> CheckHairdresserActive(CheckHairdresserActiveRequest request, ServerCallContext context)
		{
			var check = await _hairdresserAppService.CheckHairdresserActive(
				Guid.Parse(request.HairdresserId),
				DateTime.Parse(request.AppointmentDate),
				TimeSpan.Parse(request.AppointmentStartTime), 
				TimeSpan.Parse(request.ServiceDuration));

			var result = new CheckHairdresserActiveModel
			{
				IsActive = check
			};

			return result;
		}

		public override async Task<GetHairdresserOwnerIdModel> GetHairdresserOwnerId(GetHairdresserOwnerIdRequest request, ServerCallContext context)
		{
			var hairdresser = await _hairdresserAppService.GetHairdresserById(Guid.Parse(request.HairdresserId));

			var result = new GetHairdresserOwnerIdModel
			{
				OwnerId = hairdresser.OwnerId.ToString()
			};

			return result;
		}
	}
}
