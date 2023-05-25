using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Events.MassTransitOptions
{
	public static class Extensions
	{

		public static void AddMyMassTransitStateMachine<TStateMachine, TStateInstance>(this IServiceCollection services, IConfiguration Configuration, string queueName)
			where TStateMachine : class, SagaStateMachine<TStateInstance>
			where TStateInstance : class, SagaStateMachineInstance, ISagaVersion

		{
			var options = new RabbitMqOptions();
			var mongodbConn = new SagaStateDb();

			Configuration.GetSection(nameof(RabbitMqOptions)).Bind(options);
			Configuration.GetSection(nameof(SagaStateDb)).Bind(mongodbConn);

			services.Configure<RabbitMqOptions>(Configuration.GetSection(nameof(RabbitMqOptions)));
			services.Configure<SagaStateDb>(Configuration.GetSection(nameof(SagaStateDb)));

			services.AddMassTransit(x =>
			{
				//x.SetKebabCaseEndpointNameFormatter();
				x.AddSagaStateMachine<TStateMachine, TStateInstance>()
				.MongoDbRepository(r =>
				{
					r.Connection = mongodbConn.Connection;
					r.DatabaseName = mongodbConn.DatabaseName;
					r.CollectionName = mongodbConn.CollectionName;

				});

				x.AddBus(provider => MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
				{
					cfg.Host(options.RabbitMqUri);

					cfg.ReceiveEndpoint(queueName, e => e.ConfigureSaga<TStateInstance>(provider)
					);
				}));

			});

		}

		public static void AddMyMassTransit(this IServiceCollection services, IConfiguration Configuration, Assembly assembly, params (string queueName, Type consumerType)[] queueConsumerList)
		{
			var options = new RabbitMqOptions();

			Configuration.GetSection(nameof(RabbitMqOptions)).Bind(options);
			services.Configure<RabbitMqOptions>(Configuration.GetSection(nameof(RabbitMqOptions)));

			services.AddMassTransit(x =>
			{
				//x.AddBus(provider=>BUS)
				x.AddConsumers(assembly);
				x.SetKebabCaseEndpointNameFormatter();
				x.UsingRabbitMq((context, cfg) =>
				{
					//cfg.Durable = true;
					//cfg.PrefetchCount = 1;
					//cfg.PurgeOnStartup = true;
					cfg.Host(new Uri(options.RabbitMqUri));

					//cfg.ConfigureEndpoints(context);


					foreach (var item in queueConsumerList)
					{
						cfg.ReceiveEndpoint(item.queueName, e => e.ConfigureConsumer(context, item.consumerType));
					}

				});
			});

			services.AddMassTransitHostedService();
		}

	}
}
