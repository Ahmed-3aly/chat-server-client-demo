namespace CA
{
	using CA.Entities;
	using CA.Interfaces;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	public partial class ChatService :
		IChatService
	{
		public async Task<ChatRoom> CreateRoomAsync
		(
			ChatDbContext cxt,
			string roomName
		)
		{
			if (cxt == null)
			{
				throw new ArgumentException();
			}
			roomName = roomName.ValidateInputString();
			if (!roomName.StartsWith("#"))
			{
				throw new ArgumentException();
			}
			var room = await cxt
				.Rooms
				.Include(_ => _.RealtimeLog)
				.ThenInclude(_ => _.EventRoom)
				.Include(_ => _.RealtimeLog)
				.ThenInclude(_ => _.SourceUser)
				.Include(_ => _.RealtimeLog)
				.ThenInclude(_ => _.Targetuser)
				.Include(_ => _.UsersLog)
				.ThenInclude(_ => _.ChatRoomUser_User)
				.Include(_ => _.UsersLog)
				.ThenInclude(_ => _.ChatRoomUser_Room)
				.Where(_ => _.Name == roomName)
				.SingleOrDefaultAsync();
			if (room != null)
			{
				return room;
			}
			room = new ChatRoom
			{
				Name = roomName,
			};
			await cxt.Rooms.AddAsync(room);
			await cxt.SaveChangesAsync();
			await Task.Delay(100);
			return await CreateRoomAsync(cxt, roomName);
		}
	}
}
