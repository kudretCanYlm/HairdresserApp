namespace Appointment.Core.Constraints
{
	public class DateConstrains
	{
		public static bool InThisWeekAndGreaterThanNowConstrain(DateTime date)
		{
			return
				DateOnly.FromDateTime(date) >= DateOnly.FromDateTime(DateTime.Now)
				&&
				DateOnly.FromDateTime(date) <= DateOnly.FromDateTime(DateTime.Now).AddDays(7);
		}
	}
}
