using Events;
using System.Text.Json;
using Events.User.Address;

namespace User.Application.EventSourcedNormalizers.Address
{
	public static class AddressHistory
	{
		public static IList<AddressHistoryData> HistoryData { get; set; }

		public static IList<AddressHistoryData> ToJavaScriptAddressHistory(IList<StoredEvent> storedEvents)
		{
			HistoryData = new List<AddressHistoryData>();
			AddressHistoryDeserializer(storedEvents);

			var sorted = HistoryData.OrderBy(c => c.Timestamp);
			var list = new List<AddressHistoryData>();
			var last = new AddressHistoryData();

			foreach (var change in sorted)
			{
				var jsSlot = new AddressHistoryData
				{
					Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,
					City = string.IsNullOrWhiteSpace(change.City) || change.City == last.City ? "" : change.City,
					Country = string.IsNullOrWhiteSpace(change.Country) || change.Country == last.Country? "" : change.Country,
					State = string.IsNullOrWhiteSpace(change.State) || change.State == last.State ? "" : change.State,
					Street = string.IsNullOrWhiteSpace(change.Street) || change.Street == last.Street ? "" : change.Street,
					ZipCode = string.IsNullOrWhiteSpace(change.ZipCode) || change.ZipCode == last.ZipCode ? "" : change.ZipCode,
					Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
					Timestamp = change.Timestamp,
					Who = change.Who
				};

				list.Add(jsSlot);
				last = change;
			}
			return list;
		}

		private static void AddressHistoryDeserializer(IList<StoredEvent> storedEvents)
		{
			foreach (var @event in storedEvents)
			{
				var historyData = JsonSerializer.Deserialize<AddressHistoryData>(@event.Data);
				historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

				switch (@event.MessageType)
				{
					case nameof(UserAddressCreatedEvent):
						historyData.Action = "Created";
						historyData.Who = @event.User;
						break;
					case nameof(UserAddressDeletedEvent):
						historyData.Action = "Deleted";
						historyData.Who = @event.User;
						break;
					case nameof(UserAddressUpdatedEvent):
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
