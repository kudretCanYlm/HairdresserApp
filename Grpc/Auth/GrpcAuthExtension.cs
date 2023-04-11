using Grpc.Auth.ClientServices;
using Grpc.Auth.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.Auth
{
	public static class GrpcAuthExtension
	{
		public static void UseAuthMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware(typeof(AuthMiddleware));
		}
		public static void AddAuthGrpc(this IServiceCollection services,string address)
		{
			
			services.AddGrpcClient<AuthProtoService.AuthProtoServiceClient>
						(o => o.Address = new Uri(address));
			services.AddTransient<AuthGrpcService>();
		}
	}
}
