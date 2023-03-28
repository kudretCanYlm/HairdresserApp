using Events;
using Events.User;
using System.Text.Json;

namespace User.Application.EventSourcedNormalizers.User
{
	public static class UserHistory
	{
		public static IList<UserHistoryData> HistoryData { get; set; }

		public static IList<UserHistoryData> ToJavaScriptUserHistory(IList<StoredEvent> storedEvents)
		{
			HistoryData = new List<UserHistoryData>();
			UserHistoryDeserializer(storedEvents);

			var sorted = HistoryData.OrderBy(c => c.Timestamp);
			var list = new List<UserHistoryData>();
			var last = new UserHistoryData();

			foreach (var change in sorted)
			{
				var jsSlot = new UserHistoryData
				{
					Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,
					Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name ? "" : change.Name,
					aggregateId=change.aggregateId,
					Surname=string.IsNullOrWhiteSpace(change.Surname) || change.Surname == last.Surname ? "" : change.Surname,
					Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email ? "" : change.Email,
					Phone= string.IsNullOrWhiteSpace(change.Phone) || change.Phone == last.Phone ? "" : change.Phone,
					Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
					Timestamp = change.Timestamp,
					Who = change.Who
				};

				list.Add(jsSlot);
				last = change;
			}
			return list;
		}

		private static void UserHistoryDeserializer(IList<StoredEvent> storedEvents)
		{
			foreach (var @event in storedEvents)
			{
				var historyData = JsonSerializer.Deserialize<UserHistoryData>(@event.Data);
				historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");
				historyData.aggregateId = @event.AggregateId.ToString();

				switch (@event.MessageType)
				{
					case nameof(UserCreatedEvent):
						historyData.Action = "Created";
						historyData.Who = @event.User;
						break;
					case nameof(UserDeletedEvent):
						historyData.Action = "Deleted";
						historyData.Who = @event.User;
						break;
					case nameof(UserUpdatedEvent):
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
