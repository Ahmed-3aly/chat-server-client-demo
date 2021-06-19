namespace CA
{
	using CA.Entities;
	using CA.Exceptions;
	using CA.Interfaces;
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	public partial class ChatService :
		IChatService
	{
		public async Task<ChatRoom> LeaveAsync
		(
			ChatDbContext cxt,
			ChatRoom room,
			ChatUser source
		)
		{
			if (cxt == null || room == null || source == null)
			{
				throw new ArgumentException();
			}
			var roomUser =
				room
				.UsersLog
				.Where(_ => _.ChatRoomUser_UserId == source.UserId)
				.SingleOrDefault();
			if (roomUser == null)
			{
				throw new JoinRoomFirstException();
			}
			cxt
				.UsersLog
				.Remove(roomUser);
			await cxt.SaveChangesAsync();
			var e = ChatEvent.Leave
			(
				source.UserId,
				room.RoomId
			);
			return await RaiseAsync
			(
				cxt,
				room.Name,
				e
			);
		}
	}
}
