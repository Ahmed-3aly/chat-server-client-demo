namespace CA.Interfaces
{
	using CA.Entities;
	using System;
	using System.Threading.Tasks;
	public interface IChatService
	{
		Task<ChatUser> CreateUserAsync
		(
			ChatDbContext dbContext,
			string userName
		);
		Task<ChatRoom> CreateRoomAsync
		(
			ChatDbContext dbContext,
			string roomName
		);
		Task<ChatUser> RestoreUserAsync
		(
			ChatDbContext dbContext,
			string userName
		);
		Task<ChatRoom> EnterAsync
		(
			ChatDbContext dbContext,
			ChatRoom chatRoom,
			ChatUser sourceUser
		);
		Task<ChatRoom> LeaveAsync
		(
			ChatDbContext dbContext,
			ChatRoom chatRoom,
			ChatUser sourceUser
		);
		Task<ChatRoom> CommentAsync
		(
			ChatDbContext dbContext,
			ChatRoom chatRoom,
			ChatUser sourceUser,
			string commentText
		);
		Task<ChatRoom> HighFiveAsync
		(
			ChatDbContext dbContext,
			ChatRoom chatRoom,
			ChatUser sourceUser,
			string targetUserName
		);
	}
}
