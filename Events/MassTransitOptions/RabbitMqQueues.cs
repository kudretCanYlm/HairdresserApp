namespace Events.MassTransitOptions
{
	public static class RabbitMqQueues
	{
		public const string StateMachine_UserMedia = "state-machine-user-media-queue";
		public const string Media_MediaCreatedEventQueue = "media-created-event-queue";
		public const string User_UserMediaRejectedEventQueue = "user-media-rejected-event-queue";
		public const string User_UserMediaSuccessfulEventQueue = "user-media-successful-event-queue";
	}
}
