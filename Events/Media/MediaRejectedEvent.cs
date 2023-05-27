using NetDevPack.Messaging;

namespace Events.Media
{
    public class MediaRejectedEvent : Event, MassTransit.CorrelatedBy<Guid>
    {
        public MediaRejectedEvent(Guid correlationId, Guid id, Guid ownerId, string reason)
        {
            CorrelationId = correlationId;
            Id = id;
            OwnerId = ownerId;
            Reason = reason;
        }


        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Reason { get; set; }

        public Guid CorrelationId { get; }
    }
}
