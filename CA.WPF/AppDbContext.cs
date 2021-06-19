namespace CA
{
	using CA.Entities;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.IO;
	using System.Reflection;
	public class AppDbContext :
		ChatDbContext
	{
		static public readonly Func<ChatDbContext> Factory =
			() => new AppDbContext();
		static string _dbPath = "";
		readonly string FileName =
			"Data.sqlite3";
		public string DbPath { get; protected set; }
		AppDbContext()
		{
			if (string.IsNullOrEmpty(_dbPath))
			{
				var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\";
				_dbPath = path + FileName;
				if (File.Exists(_dbPath))
				{
					File.Delete(_dbPath);
				}
			}
			DbPath = _dbPath;
			Database.EnsureCreated();
		}
		protected override void OnConfiguring
		(
			DbContextOptionsBuilder optionsBuilder
		)
		{
			optionsBuilder.UseSqlite($"Filename={DbPath}");
		}
	}
}
