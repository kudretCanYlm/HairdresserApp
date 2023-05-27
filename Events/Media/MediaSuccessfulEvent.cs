using NetDevPack.Messaging;

namespace Events.Media
{
    public class MediaSuccessfulEvent : Event, MassTransit.CorrelatedBy<Guid>
    {
        public MediaSuccessfulEvent(Guid correlationId, Guid id, Guid ownerId)
        {
            CorrelationId = correlationId;
            Id = id;
            OwnerId = ownerId;
        }

        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }

        public Guid CorrelationId { get; }
    }
}
