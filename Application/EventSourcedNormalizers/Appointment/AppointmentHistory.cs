using Events;
using System.Text.Json;

namespace Appointment.Application.EventSourcedNormalizers.Appointment
{
	public static class AppointmentHistory
	{
		public static IList<AppointmentHistoryData> HistoryData { get; set; }

		public static IList<AppointmentHistoryData> ToJavaScriptAppointmentHistory(IList<StoredEvent> storedEvents)
		{
			HistoryData = new List<AppointmentHistoryData>();
			AppointmentHistoryDeserializer(storedEvents);

			var sorted = HistoryData.OrderBy(c => c.Timestamp);
			var list = new List<AppointmentHistoryData>();
			var last = new AppointmentHistoryData();

			foreach (var change in sorted)
			{
				var jsSlot = new AppointmentHistoryData
				{
					Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,
					AppointmentState = string.IsNullOrWhiteSpace(change.AppointmentState) || change.AppointmentState == last.AppointmentState ? "" : change.AppointmentState,
					Notes= string.IsNullOrWhiteSpace(change.Notes) || change.Notes == last.Notes ? "" : change.Notes,
					AppointmentDate = change.AppointmentDate == null || change.AppointmentDate == last.AppointmentDate ? null : change.AppointmentDate,
					AppointmentStartTime = change.AppointmentStartTime == null || change.AppointmentStartTime == last.AppointmentStartTime ? null : change.AppointmentStartTime,
					AppointmentEndTime = change.AppointmentEndTime == null || change.AppointmentEndTime == last.AppointmentEndTime ? null : change.AppointmentEndTime,
					UserId = change.UserId == Guid.Empty.ToString() || change.UserId == last.UserId ? "" : change.UserId,
					HairdresserServiceId = change.HairdresserServiceId == Guid.Empty.ToString() || change.HairdresserServiceId == last.HairdresserServiceId ? "" : change.HairdresserServiceId,
					HairdresserId = change.HairdresserId == Guid.Empty.ToString() || change.HairdresserId == last.HairdresserId ? "" : change.HairdresserId,
					Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
					Timestamp = change.Timestamp,
					Who = change.Who
				};

				list.Add(jsSlot);
				last = change;
			}
			return list;
		}


		private static void AppointmentHistoryDeserializer(IList<StoredEvent> storedEvents)
		{
			foreach (var @event in storedEvents)
			{
				var historyData = JsonSerializer.Deserialize<AppointmentHistoryData>(@event.Data);
				historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

				historyData.Action = nameof(@event.MessageType);
				historyData.Who = @event.User;

				HistoryData.Add(historyData);

			}
		}
	}
}
