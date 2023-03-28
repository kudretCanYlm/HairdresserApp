using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.PaggingAndFilter
{
	public class SortingOption
	{
		public string Field { get; set; }

		public SortingDirection Direction { get; set; }

		public int Priority { get; set; }
	}
	public enum SortingDirection
	{
		ASC,
		DESC
	}
}
