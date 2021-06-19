namespace CA.Tests
{
	using CA.Entities;
	using CA.Exceptions;
	using System;
	using System.Linq;
	using Xunit;
	public class LeaveTests
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
					.LeaveAsync(null, room, user);
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
					.LeaveAsync(cxt, null, user);
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
					.LeaveAsync(cxt, room, null);
			});
		}
		[Fact]
		public async void Leave_BeforeJoin_Throws_JoinRoomFirstException()
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
					.LeaveAsync(cxt, room, user);
			});
		}
		[Fact]
		public async void Leave_Returns_ChatRoom()
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
				.LeaveAsync(cxt, room, user);
			// Assert
			Assert.NotNull(newRoom);
			Assert.IsType<ChatRoom>(newRoom);
			Assert.True(!newRoom.UsersLog.Where(_ => _.ChatRoomUser_UserId == user.UserId).Any());
		}
	}
}
