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
		public async Task<ChatRoom> CommentAsync
		(
			ChatDbContext cxt,
			ChatRoom room,
			ChatUser source,
			string text
		)
		{
			if (cxt == null || room == null || source == null)
			{
				throw new ArgumentException();
			}
			var roomUser =
				room.UsersLog
				.Where(_ => _.ChatRoomUser_UserId == source.UserId)
				.SingleOrDefault();
			if (roomUser == null)
			{
				throw new JoinRoomFirstException();
			}
			var e = ChatEvent.Comment
			(
				source.UserId,
				room.RoomId,
				text
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
