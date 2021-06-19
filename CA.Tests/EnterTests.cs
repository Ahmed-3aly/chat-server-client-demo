namespace CA.Tests
{
	using CA.Entities;
	using System;
	using System.Linq;
	using Xunit;
	public class EnterTests
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
			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async() =>
			{
				// Act
				var newRoom = await ChatService
					.Instance
					.EnterAsync(null, room, user);
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
			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				var newRoom = await ChatService
					.Instance
					.EnterAsync(cxt, null, user);
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
			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				var newRoom = await ChatService
					.Instance
					.EnterAsync(cxt, room, null);
			});
		}
		[Fact]
		public async void Enter_Returns_ChatRoom()
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			var room = await ChatService
				.Instance
				.CreateRoomAsync(cxt, TestsData.RoomName);
			var user = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[0]);
			// Act
			var newRoom = await ChatService
				.Instance
				.EnterAsync(cxt, room, user);
			// Assert
			Assert.NotNull(newRoom);
			Assert.IsType<ChatRoom>(newRoom);
			Assert.True(newRoom.UsersLog.Where(_ => _.ChatRoomUser_UserId == user.UserId).Any());
		}
	}
}
