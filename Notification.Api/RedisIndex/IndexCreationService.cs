using Notification.Api.Models;
using Redis.OM;

namespace Notification.Api.RedisIndex
{
	public class IndexCreationService:IHostedService
	{
		private readonly RedisConnectionProvider _provider;
		public IndexCreationService(RedisConnectionProvider provider)
		{
			_provider = provider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			var info = (await _provider.Connection.ExecuteAsync("FT._LIST")).ToArray().Select(x => x.ToString());

			if (info.All(x => x != "appointmentmodel-idx"))
			{
				await _provider.Connection.CreateIndexAsync(typeof(AppointmentModel));
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
