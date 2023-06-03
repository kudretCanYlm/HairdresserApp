﻿using NetDevPack.Messaging;

namespace Events.Media
{
	public class MediaUpdatedEvent : Event, MassTransit.CorrelatedBy<Guid>
	{
		public MediaUpdatedEvent()
		{

		}

		public MediaUpdatedEvent(Guid correlationId, Guid id, string fileExtension, byte[] mediaData, string customType, Guid imageOwnerId)
		{
			CorrelationId = correlationId;
			Id = id;
			AggregateId = id;
			FileExtension = fileExtension;
			MediaData = mediaData;
			CustomType = customType;
			ImageOwnerId = imageOwnerId;
		}

		public Guid Id { get; set; }
		public string FileExtension { get; set; }
		public byte[] MediaData { get; set; }
		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
		public Guid CorrelationId { get; }
	}
}
