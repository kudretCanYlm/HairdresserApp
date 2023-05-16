using Events;
using Events.Hairdresser;
using System.Text.Json;

namespace Hairdresser.Application.EventSourcedNormalizers.Hairdresser
{
	public static class HairdresserHistory
	{
		public static IList<HairdresserHistoryData> HistoryData { get; set; }

		public static IList<HairdresserHistoryData> ToJavaScriptAddressHistory(IList<StoredEvent> storedEvents)
		{
			HistoryData = new List<HairdresserHistoryData>();
			HairdresserHistoryDeserializer(storedEvents);

			var sorted = HistoryData.OrderBy(c => c.Timestamp);
			var list = new List<HairdresserHistoryData>();
			var last = new HairdresserHistoryData();

			foreach (var change in sorted)
			{
				var jsSlot = new HairdresserHistoryData
				{
					Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,
					Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name ? "" : change.Name,
					About = string.IsNullOrWhiteSpace(change.About) || change.About == last.About ? "" : change.About,
					Address = string.IsNullOrWhiteSpace(change.Address) || change.Address == last.Address ? "" : change.Address,
					Coordinate = string.IsNullOrWhiteSpace(change.Coordinate) || change.Coordinate == last.Coordinate ? "" : change.Coordinate,
					IsOpenNow = change.IsOpenNow == null || change.IsOpenNow == last.IsOpenNow ? null : change.IsOpenNow,
					OpenHour = change.OpenHour == null || change.OpenHour == last.OpenHour ? null : change.OpenHour,
					CloseHour = change.CloseHour == null || change.CloseHour == last.CloseHour ? null : change.CloseHour,
					WorkDays = string.IsNullOrWhiteSpace(change.WorkDays) || change.WorkDays == last.WorkDays ? "" : change.WorkDays,
					OwnerId = change.OwnerId == Guid.Empty.ToString() || change.OwnerId == last.OwnerId ? "" : change.OwnerId,
					Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
					Timestamp = change.Timestamp,
					Who = change.Who
				};

				list.Add(jsSlot);
				last = change;
			}
			return list;
		}


		private static void HairdresserHistoryDeserializer(IList<StoredEvent> storedEvents)
		{
			foreach (var @event in storedEvents)
			{
				var historyData = JsonSerializer.Deserialize<HairdresserHistoryData>(@event.Data);
				historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

				switch (@event.MessageType)
				{
					//try later
					//case nameof(@event.MessageType).Contains("Created")
					case nameof(HairdresserCreatedEvent):
						historyData.Action = "Created";
						historyData.Who = @event.User;
						break;
					case nameof(HairdresserDeletedEvent):
						historyData.Action = "Deleted";
						historyData.Who = @event.User;
						break;
					case nameof(HairdresserUpdatedEvent):
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
