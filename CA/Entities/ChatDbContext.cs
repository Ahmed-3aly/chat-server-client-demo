namespace CA.Entities
{
	using Microsoft.EntityFrameworkCore;
	public abstract class ChatDbContext :
		DbContext
	{
		public DbSet<ChatUser> Users { get; set; }
		public DbSet<ChatRoom> Rooms { get; set; }
		public DbSet<ChatEvent> EventsLog { get; set; }
		public DbSet<ChatRoomUser> UsersLog { get; set; }
		public ChatDbContext() { }
		public ChatDbContext
		(
			DbContextOptions<ChatDbContext> options
		) : base(options)
        {

        }

	}
}
