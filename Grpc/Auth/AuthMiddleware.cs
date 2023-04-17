using Grpc.Auth.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Security.Claims;

namespace Grpc.Auth
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly AuthGrpcService _authGrpcService;
		private readonly ILogger<AuthMiddleware> _logger;

		public AuthMiddleware(RequestDelegate next, AuthGrpcService authGrpcService, ILogger<AuthMiddleware> logger)
		{
			_next = next;
			_authGrpcService = authGrpcService;
			_logger = logger;
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


				var user = await _authGrpcService.CheckTokenRefreshAndUserId(token);

				if (user != null)
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
					_logger.LogError("auth error");

					httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
				}
			}
			else
			{
				_logger.LogError("auth error");
				httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
			}

		}
	}

}
