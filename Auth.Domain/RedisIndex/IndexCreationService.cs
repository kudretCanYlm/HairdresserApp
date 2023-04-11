using Auth.Domain.Models;
using Microsoft.Extensions.Hosting;
using Redis.OM;
using System;

namespace Auth.Domain.RedisIndex
{
	public class IndexCreationService : IHostedService
	{
		private readonly RedisConnectionProvider _provider;
		public IndexCreationService(RedisConnectionProvider provider)
		{
			_provider = provider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			var info = (await _provider.Connection.ExecuteAsync("FT._LIST")).ToArray().Select(x => x.ToString());

			if (info.All(x => x != "authsessionmodel-idx"))
			{
				await _provider.Connection.CreateIndexAsync(typeof(AuthSessionModel));
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
