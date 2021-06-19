namespace CA.DTOs
{
	using CA.Entities;
	using CA.Enums;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public struct HourlyStats
	{
		public DateTime DateHour { get; private set; }
		public Dictionary<long, ChatEvent> Raw { get; private set; }
		public int Enters { get; private set; }
		public int Leaves { get; private set; }
		public int Comments { get; private set; }
		public Tuple<int, int> Highfives { get; private set; }
		public string Print
		{
			get
			{
				var result = DateHour.PrintDateTime();
				if (Enters > 0)
				{
					result +=
						Environment.NewLine +
						Helpers.PrintPeople(Enters) +
						" entered";
				}
				if (Leaves > 0)
				{
					result +=
						Environment.NewLine +
						Helpers.PrintPeople(Leaves) +
						" left";
				}
				if (Comments > 0)
				{
					result +=
						Environment.NewLine +
						Helpers.PrintPeople(Comments) +
						" commented";
				}
				if (Highfives.Item1 > 0)
				{
					result +=
						Environment.NewLine +
						Helpers.PrintPeople(Highfives.Item1) +
						" highfived " +
						Helpers.PrintPeople(Highfives.Item2, false).Replace(" ", " other ");
				}
				return result;
			}
		}
		public HourlyStats
		(
			DateTime dateHour
		)
		{
			DateHour = dateHour;
			Enters = 0;
			Leaves = 0;
			Comments = 0;
			Highfives = new Tuple<int, int>(0, 0);
			Raw = new Dictionary<long, ChatEvent>();
		}
		public void AddRange
		(
			List<ChatEvent> v
		)
		{
			foreach (var i in v)
			{
				if (Raw.ContainsKey(i.EventId))
				{
					continue;
				}
				Raw.Add(i.EventId, i);
			}
			Enters = Raw
				.Values
				.Where(_ => _.EventType == ChatEventEnum.Enter)
				.Select(_ => _.SourceUserId)
				.Distinct()
				.Count();
			Leaves = Raw
				.Values
				.Where(_ => _.EventType == ChatEventEnum.Leave)
				.Select(_ => _.SourceUserId)
				.Distinct()
				.Count();
			Comments = Raw
				.Values
				.Where(_ => _.EventType == ChatEventEnum.Comment)
				.Select(_ => _.SourceUserId)
				.Distinct()
				.Count();
			var que =
				Raw
				.Values
				.Where(_ => _.EventType == ChatEventEnum.HighFive)
				.ToList();
			var a = que.Select(_ => _.SourceUserId).Distinct().Count();
			var b = que.Select(_ => _.TargetUserId).Distinct().Count();
			Highfives = new Tuple<int, int>(a, b);
		}
	}
}
