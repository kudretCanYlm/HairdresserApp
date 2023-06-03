using Grpc.Core;
using Grpc.HairdresserService.Protos;
using HairdresserService.Application.Interfaces.HairdresserService;

namespace HairdresserService.GRPC.Services
{
	public class HairdresserServiceService : HairdresserServiceProtoService.HairdresserServiceProtoServiceBase
	{
		private readonly IHairdresserServiceAppService _hairdresserServiceAppService;

		public HairdresserServiceService(IHairdresserServiceAppService hairdresserServiceAppService)
		{
			_hairdresserServiceAppService = hairdresserServiceAppService;
		}

		public override async Task<HairdresserServiceModel> GetHairdresserService(GetHairdresserServiceRequest request, ServerCallContext context)
		{
			var hairdresserService = await _hairdresserServiceAppService.GetHairdresserServiceByIdAndHairdresserId(Guid.Parse(request.ServiceId), Guid.Parse(request.HairdresserId));

			var hairdresserServiceModel = new HairdresserServiceModel
			{
				Price = hairdresserService != null ? double.Parse(hairdresserService.Price.ToString()) : 0,
				ServiceDuration = hairdresserService != null ? hairdresserService.ServiceDuration.ToString() : "00:00:00",
				IsAny = hairdresserService != null ? true : false
			};

			return hairdresserServiceModel;
		}

		public async override Task<GetHairdresserServiceNameByIdModel> GetHairdresserServiceNameById(GetHairdresserServiceRequest request, ServerCallContext context)
		{
			var hairdresserService = await _hairdresserServiceAppService.GetHairdresserServiceByIdAndHairdresserId(Guid.Parse(request.ServiceId), Guid.Parse(request.HairdresserId));

			var getHairdresserServiceNameByIdModel = new GetHairdresserServiceNameByIdModel
			{
				ServiceName = hairdresserService?.Name ?? "Empty"
			};

			return getHairdresserServiceNameByIdModel;
		}

	}
}
