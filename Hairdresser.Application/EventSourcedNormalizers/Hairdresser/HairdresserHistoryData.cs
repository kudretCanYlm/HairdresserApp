using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdresser.Application.EventSourcedNormalizers.Hairdresser
{
	public class HairdresserHistoryData
	{
		public string Action { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public string About { get; set; }
		public string Address { get; set; }
		public string Coordinate { get; set; }
		public bool? IsOpenNow { get; set; }
		public TimeSpan? OpenHour { get; set; }
		public TimeSpan? CloseHour { get; set; }
		public string WorkDays { get; set; }
		public string OwnerId { get; set; }
		public string Timestamp { get; set; }
		public string Who { get; set; }
	}
}
