using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Consul
{

	public class ConsulRegisterService : IHostedService
	{
		private IConsulClient _consulClient;
		private ConsulConfiguration _consulConfiguration;
		private ServiceConfiguration _serviceConfiguration;
		private ILogger<ConsulRegisterService> _logger;

		public ConsulRegisterService(IConsulClient consulClient,
			IOptions<ServiceConfiguration> serviceConfiguration,
			IOptions<ConsulConfiguration> consulConfiguration,
			ILogger<ConsulRegisterService> logger)
		{
			_consulClient = consulClient;
			_consulConfiguration = consulConfiguration.Value;
			_serviceConfiguration = serviceConfiguration.Value;
			_logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			var serviceUri = new Uri(_serviceConfiguration.Url);

			var serviceRegistration = new AgentServiceRegistration()
			{
				Address = serviceUri.Host,
				Name = _serviceConfiguration.ServiceName,
				Port = serviceUri.Port,
				ID = _serviceConfiguration.ServiceId,
				Tags = new[] { _serviceConfiguration.ServiceName }
			};

			await _consulClient.Agent.ServiceDeregister(_serviceConfiguration.ServiceId, cancellationToken);
			await _consulClient.Agent.ServiceRegister(serviceRegistration, cancellationToken);

		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			try
			{
				await _consulClient.Agent.ServiceDeregister(_serviceConfiguration.ServiceId, cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error when trying to de-register", ex);
			}
		}
	}

}