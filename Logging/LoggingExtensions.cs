using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Microsoft.Extensions.Logging;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Builder;
using Elastic.Apm.NetCoreAll;
using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Elasticsearch;

namespace MEES.Infrastructure.Logging
{
	public static class LoggingExtensions
	{
		public static Logger AddMyLogging(IConfiguration configuration)
		{
			var logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithExceptionDetails()
				.Enrich.WithCorrelationId()
				.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
			return logger;
		}

		public static IApplicationBuilder UseMyLogging(this IApplicationBuilder app, IConfiguration Configuration, ILoggerFactory loggerFactory)
		{
			app.UseAllElasticApm(Configuration);

			loggerFactory.AddSerilog();

			return app;
		}
	}
}
