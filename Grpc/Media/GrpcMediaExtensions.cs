using Grpc.Media.ClientServices;
using Grpc.Media.Protos;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.Media
{
	public static class GrpcMediaExtensions
	{
		public static void AddMediaGrpc(this IServiceCollection services, string address)
		{
			services.AddGrpcClient<MediaProtoService.MediaProtoServiceClient>
				(o => o.Address = new Uri(address));
			services.AddTransient<MediaGrpcService>();
		}
	}
}
