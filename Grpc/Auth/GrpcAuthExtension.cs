using Grpc.Auth.ClientServices;
using Grpc.Auth.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

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

		public static Guid? GetCurrentUserId(this HttpContext context)
		{
			var userId=context.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
			Guid userIdGuid;

			if (Guid.TryParse(userId, out userIdGuid))
				return userIdGuid;

			return null;
		}
	}
}
