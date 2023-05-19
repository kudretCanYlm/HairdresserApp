using Events;
using Events.Media;
using System.Text.Json;

namespace Media.Application.EventSourcedNormalizers.Media
{
	public static class MediaHistory
	{
		public static IList<MediaHistoryData> HistoryData { get; set; }


		public static IList<MediaHistoryData> ToJavaScriptMediaHistory(IList<StoredEvent> storedEvents)
		{
			HistoryData = new List<MediaHistoryData>();
			MediaHistoryDeserializer(storedEvents);

			var sorted = HistoryData.OrderBy(c => c.Timestamp);
			var list = new List<MediaHistoryData>();
			var last = new MediaHistoryData();

			foreach (var change in sorted)
			{
				var jsSlot = new MediaHistoryData
				{
					Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,
					FileExtension = string.IsNullOrWhiteSpace(change.FileExtension) || change.FileExtension == last.FileExtension ? "" : change.FileExtension,
					MediaData = change.MediaData.Length>0 || change.MediaData.SequenceEqual(last.MediaData) ? Array.Empty<byte>() : change.MediaData,	
					CustomType = string.IsNullOrWhiteSpace(change.CustomType) || change.CustomType == last.CustomType ? "" : change.CustomType,
					ImageOwnerId = change.ImageOwnerId == Guid.Empty.ToString() || change.ImageOwnerId == last.ImageOwnerId ? "" : change.ImageOwnerId,
					Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
					Timestamp = change.Timestamp,
					Who = change.Who
				};

				list.Add(jsSlot);
				last = change;
			}
			return list;
		}


		private static void MediaHistoryDeserializer(IList<StoredEvent> storedEvents)
		{
			foreach (var @event in storedEvents)
			{
				var historyData = JsonSerializer.Deserialize<MediaHistoryData>(@event.Data);
				historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

				switch (@event.MessageType)
				{
					//try later
					//case nameof(@event.MessageType).Contains("Created")
					case nameof(MediaCreatedEvent):
						historyData.Action = "Created";
						historyData.Who = @event.User;
						break;
					case nameof(MediaDeletedEvent):
						historyData.Action = "Deleted";
						historyData.Who = @event.User;
						break;
					case nameof(MediaUpdatedEvent):
						historyData.Action = "Updated";
						historyData.Who = @event.User;
						break;
					default:
						historyData.Action = "Unrecognized";
						historyData.Who = @event.User ?? "Anonymous";
						break;
				}
				HistoryData.Add(historyData);

			}
		}
	}
}
