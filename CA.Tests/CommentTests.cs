namespace CA.Tests
{
	using CA.Entities;
	using CA.Exceptions;
	using System;
	using System.Linq;
	using Xunit;
	public class CommentTests
	{
		[Fact]
		public async void NoContext_Throws_ArgumentException()
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			var room = await ChatService
				.Instance
				.CreateRoomAsync(cxt, TestsData.RoomName);
			var user = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[0]);
			await ChatService
				.Instance
				.EnterAsync(cxt, room, user);
			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async() =>
			{
				// Act
				var newRoom = await ChatService
					.Instance
					.CommentAsync(null, room, user, TestsData.RandomName);
			});
		}
		[Fact]
		public async void NoRoom_Throws_ArgumentException()
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			var room = await ChatService
				.Instance
				.CreateRoomAsync(cxt, TestsData.RoomName);
			var user = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[0]);
			await ChatService
				.Instance
				.EnterAsync(cxt, room, user);
			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				var newRoom = await ChatService
					.Instance
					.CommentAsync(cxt, null, user, TestsData.RandomName);
			});
		}
		[Fact]
		public async void NoUser_Throws_ArgumentException()
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			var room = await ChatService
				.Instance
				.CreateRoomAsync(cxt, TestsData.RoomName);
			var user = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[0]);
			await ChatService
				.Instance
				.EnterAsync(cxt, room, user);
			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				var newRoom = await ChatService
					.Instance
					.CommentAsync(cxt, room, null, TestsData.RandomName);
			});
		}
		[Fact]
		public async void Comment_BeforeJoin_Throws_JoinRoomFirstException()
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			var room = await ChatService
				.Instance
				.CreateRoomAsync(cxt, TestsData.RoomName);
			var user = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[0]);
			// Assert
			await Assert.ThrowsAsync<JoinRoomFirstException>(async () =>
			{
				// Act
				var newRoom = await ChatService
					.Instance
					.CommentAsync(cxt, room, user, TestsData.RandomName);
			});
		}
		[Fact]
		public async void Comment_Returns_ChatRoom()
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			var room = await ChatService
				.Instance
				.CreateRoomAsync(cxt, TestsData.RoomName);
			var user = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[0]);
			await ChatService
				.Instance
				.EnterAsync(cxt, room, user);
			// Act
			var newRoom = await ChatService
				.Instance
				.CommentAsync(cxt, room, user, TestsData.RandomName);
			// Assert
			Assert.NotNull(newRoom);
			Assert.IsType<ChatRoom>(newRoom);
			Assert.True(newRoom.RealtimeLog.Last().SourceUserId == user.UserId);
			Assert.True(newRoom.RealtimeLog.Last().EventType == Enums.ChatEventEnum.Comment);
		}
	}
}
