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
		public async Task<ChatRoom> HighFiveAsync
		(
			ChatDbContext cxt,
			ChatRoom room,
			ChatUser source,
			string targetName
		)
		{
			if (cxt == null || room == null || source == null)
			{
				throw new ArgumentException();
			}
			targetName = targetName.ValidateInputString();
			var targetUser = (ChatRoomUser)null;
			var sourceUser =
				room
				.UsersLog
				.Where(_ => _.ChatRoomUser_UserId == source.UserId)
				.SingleOrDefault();
			if (sourceUser == null)
			{
				throw new JoinRoomFirstException();
			}
			targetUser =
				room
				.UsersLog
				.Where(_ => _.ChatRoomUser_User.Name.ToLower() == targetName.ToLower())
				.SingleOrDefault();
			if (targetUser == null)
			{
				throw new InvalidUserNameException();
			}
			var e = ChatEvent.HighFive
			(
				source.UserId,
				room.RoomId,
				targetUser.ChatRoomUser_UserId
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
