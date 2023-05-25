namespace Events.MassTransitOptions
{
	public class RabbitMqOptions
	{
		public string RabbitMqUri { get; set; } = "amqp://guest:guest@localhost:5672/";

		
		//public static string ReportRequestServiceQueue = "registerorder.service";
		
		//public string SagaQueue = "saga.service";
	}
}
