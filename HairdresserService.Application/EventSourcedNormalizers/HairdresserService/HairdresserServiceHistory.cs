using Events;
using System.Text.Json;

namespace HairdresserService.Application.EventSourcedNormalizers.HairdresserService
{
	public static class HairdresserServiceHistory
	{
		public static IList<HairdresserServiceHistoryData> HistoryData { get; set; }

		public static IList<HairdresserServiceHistoryData> ToJavaScriptHairdresserServiceHistory(IList<StoredEvent> storedEvents)
		{
			HistoryData = new List<HairdresserServiceHistoryData>();
			HairdresserServiceHistoryDeserializer(storedEvents);

			var sorted = HistoryData.OrderBy(c => c.Timestamp);
			var list = new List<HairdresserServiceHistoryData>();
			var last = new HairdresserServiceHistoryData();

			foreach (var change in sorted)
			{
				var jsSlot = new HairdresserServiceHistoryData
				{
					Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,
					Name= string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name ? "" : change.Name,
					Price=change.Price==null || change.Price==last.Price?null:change.Price,
					ServiceDuration= change.ServiceDuration == null || change.ServiceDuration == last.ServiceDuration ? null : change.ServiceDuration,
					HairdresserId= change.HairdresserId == Guid.Empty.ToString() || change.HairdresserId == last.HairdresserId ? "" : change.HairdresserId,
					IsItActive=change.IsItActive==null || change.IsItActive==last.IsItActive? null: change.IsItActive,
					Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
					Timestamp = change.Timestamp,
					Who = change.Who
				};

				list.Add(jsSlot);
				last = change;
			}
			return list;
		}

		private static void HairdresserServiceHistoryDeserializer(IList<StoredEvent> storedEvents)
		{
			foreach (var @event in storedEvents)
			{
				var historyData = JsonSerializer.Deserialize<HairdresserServiceHistoryData>(@event.Data);
				historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

				historyData.Action = nameof(@event.MessageType);
				historyData.Who = @event.User;

				HistoryData.Add(historyData);

			}
		}
	}
}
