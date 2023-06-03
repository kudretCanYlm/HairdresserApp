namespace Events.MassTransitOptions
{
	public static class RabbitMqQueues
	{
		public const string StateMachine_UserMedia = "state-machine-user-media-queue";
		public const string StateMachine_HairdresserMedia = "state-machine-hairresser-media-queue";
		public const string StateMachine_HairdresserServiceMedia = "state-machine-hairresserservice-media-queue";
		public const string Media_MediaCreatedEventQueue = "media-created-event-queue";
		public const string Media_MediaDeletedEventQueue = "media-deleted-event-queue";
		public const string Media_MediaUpdatedEventQueue = "media-updated-event-queue";

		public const string Appointment_AppointmentCreatedEventQueue = "appointment-created-event-queue";
		public const string Appointment_AppointmentApprovedEventQueue = "appointment-approve-event-queue";
	}
}
