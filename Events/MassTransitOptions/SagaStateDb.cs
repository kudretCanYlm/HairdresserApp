namespace Events.MassTransitOptions
{
	public class SagaStateDb
	{
		public string Connection { get; set; }
		public string DatabaseName { get; set; }
		public string CollectionName { get; set; }
	}
}
