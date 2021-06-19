namespace CA.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class TestsData
	{
		static public string RandomName =>
			string.Join
			(
				"",
				Guid.NewGuid().ToString().Replace("-", "").Take(8)
			);
		public static string RoomName =>
			"#" + RandomName;
		public static readonly List<string> UserNames =
			new List<string>
			{
				"Alice",
				"Bob",
				"Eve",
			};
		public static IEnumerable<object[]> RoomName_Valid =>
			new List<object[]>
			{
				new object[] { RoomName },
			};
		public static readonly IEnumerable<object[]> UserName_Valid =
			new List<object[]>
			{
				new object[] { UserNames[0] },
				new object[] { UserNames[1] },
				new object[] { UserNames[2] },
			};
		public static readonly IEnumerable<object[]> UserName_Invalid =
			new List<object[]>
			{
				new object[] { "" },
				new object[] { "__" },
				new object[] { "_____________" },
				new object[] { "___ ___" },
			};
		public static readonly IEnumerable<object[]> RoomName_Invalid =
			new List<object[]>
			{
				new object[] { "" },
				new object[] { "#_" },
				new object[] { "#____________" },
				new object[] { "#__ ___" },
				new object[] { "_______" },
			};
	}
}
