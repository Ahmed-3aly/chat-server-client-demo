namespace CA.Tests
{
	using CA.Entities;
	using Microsoft.EntityFrameworkCore;
	using System;
	internal class TestDbContext :
		ChatDbContext
	{
		static DbContextOptionsBuilder<ChatDbContext> _optionsBuilder { get; set; }
		static TestDbContext()
		{
			var builder = new DbContextOptionsBuilder<ChatDbContext>();
			builder.EnableSensitiveDataLogging(true);
			_optionsBuilder = builder;
		}
		TestDbContext() :
			base
			(
				_optionsBuilder
				.UseInMemoryDatabase(
					databaseName: TestsData.RandomName
				).Options
			)
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}
		static public readonly Func<ChatDbContext> Factory =
			() => new TestDbContext();
	}
}
