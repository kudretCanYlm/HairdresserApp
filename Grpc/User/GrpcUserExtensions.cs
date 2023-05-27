using Grpc.User.ClientServices;
using Grpc.User.Protos;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.User
{
	public static class GrpcUserExtensions
	{
		public static void AddUserGrpc(this IServiceCollection services,string address)
		{
			services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
					(o => o.Address = new Uri(address));

			services.AddTransient<UserGrpcService>();
		}
	}
}
