using Grpc.HairdresserService.ClientServices;
using Grpc.HairdresserService.Protos;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.HairdresserService
{
	public static class GrpcHairdresserServiceExtensions
	{
		public static void AddHairdresserServiceGrpc(this IServiceCollection services, string address)
		{
			services.AddGrpcClient<HairdresserServiceProtoService.HairdresserServiceProtoServiceClient>
				(o => o.Address = new Uri(address));
			services.AddTransient<HairdresserServiceGrpcService>();
		}
	}
}
