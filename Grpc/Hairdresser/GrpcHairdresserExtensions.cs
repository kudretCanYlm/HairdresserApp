using Grpc.Hairdresser.ClientServices;
using Grpc.Hairdresser.Protos;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.Hairdresser
{
	public static class GrpcHairdresserExtensions
	{
		public static void AddHairdresserGrpc(this IServiceCollection services, string address)
		{
			services.AddGrpcClient<HairdresserProtoService.HairdresserProtoServiceClient>
				(o => o.Address = new Uri(address));
			services.AddTransient<HairdresserGrpcService>();
		}
	}
}
