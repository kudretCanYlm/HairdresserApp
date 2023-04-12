using Grpc.Auth.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

namespace Grpc.Auth
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly AuthGrpcService _authGrpcService;

		public AuthMiddleware(RequestDelegate next, AuthGrpcService authGrpcService)
		{
			_next = next;
			_authGrpcService = authGrpcService;
		}

		public async Task Invoke(HttpContext httpContext)
		{

			var endpoint = httpContext.GetEndpoint();
			if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
			{
				await _next(httpContext);
				return;
			}

			string authHeader = httpContext.Request.Headers["Authorization"];

				if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
				{
					var token = authHeader.Split(' ')?[1]?.Trim();


					var user=await _authGrpcService.CheckTokenRefreshAndUserId(token);

					if (user!=null)
					{
						var claims = new[]
						{
							new Claim(ClaimTypes.NameIdentifier, user.UserId),
							new Claim(ClaimTypes.Role, "Admin")
						};
						var identity = new ClaimsIdentity(claims, "Bearer");
						httpContext.User = new ClaimsPrincipal(identity);

						await _next(httpContext);
					}
					else
					{
						httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
					}
				}
				else
				{
					httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
				}
			


		}
	}

	public class AuthMiddleware2 : IMiddleware
	{
		private readonly AuthGrpcService _authGrpcService;

		public AuthMiddleware2(AuthGrpcService authGrpcService)
		{
			_authGrpcService = authGrpcService;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var endpoint = context.GetEndpoint();
			if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
			{
				await next(context);
				return;
			}

			string authHeader = context.Request.Headers["Authorization"];

			try
			{
				if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
				{
					var token = authHeader.Split(' ')[1].Trim();


					var user = await _authGrpcService.CheckTokenRefreshAndUserId(token);

					if (user != null)
					{
						var claims = new[]
						{
							new Claim("UserId", user.UserId),
							new Claim(ClaimTypes.Role, "Admin")
						};
						var identity = new ClaimsIdentity(claims, "Basic");
						context.User = new ClaimsPrincipal(identity);

						await next(context);
					}
					else
					{
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					}
				}
				else
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				}
			}
			catch
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			}
		}
	}

}
