namespace CA
{
	using System;
	static internal class Helpers
	{
		static public DateTime DateHour
		(
			this DateTime v
		) => v
			.Date
			.AddHours(v.Hour);
		static public string PrintDateTime
		(
			this DateTime v
		)
		{
			var a = DateTime.Now;
			var b = DateTime.UtcNow;
			var c = a.Subtract(b);
			c = TimeSpan.FromMinutes(Math.Round(c.TotalMinutes));
			v = v.Add(c);
			return v
				.ToLocalTime()
				.ToShortDateString() +
				" " +
				v.ToShortTimeString().ToUpper();
		}
		static public string PrintPeople
		(
			int n,
			bool prefix = true
		)
		{
			var p = prefix ? "\t" : "";
			return p + n + " " + (n == 1 ? "person" : "people");
		}
		static public string ValidateInputString
		(
			this string v
		)
		{
			v = v.Trim();
			var n = v.Length;
			if
			(
				string.IsNullOrEmpty(v) ||
				v.Contains(" ") ||
				n < Constants.MinLen ||
				n > Constants.MaxLen
			)
			{
				throw new ArgumentException();
			}
			return v;
		}
	}
}
