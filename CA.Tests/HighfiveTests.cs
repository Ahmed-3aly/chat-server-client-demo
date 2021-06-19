namespace CA.Tests
{
	using CA.Entities;
	using CA.Exceptions;
	using System;
	using System.Linq;
	using Xunit;
	public class HighfiveTests
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
					.HighFiveAsync(null, room, user, TestsData.UserNames[1]);
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
					.HighFiveAsync(cxt, null, user, TestsData.UserNames[1]);
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
					.HighFiveAsync(cxt, room, null, TestsData.UserNames[1]);
			});
		}
		[Fact]
		public async void Highfive_BeforeJoin_Throws_JoinRoomFirstException()
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
					.HighFiveAsync(cxt, room, user, TestsData.UserNames[1]);
			});
		}
		[Fact]
		public async void Highfive_Returns_ChatRoom()
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			var room = await ChatService
				.Instance
				.CreateRoomAsync(cxt, TestsData.RoomName);
			var user_1 = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[0]);
			var user_2 = await ChatService
				.Instance
				.CreateUserAsync(cxt, TestsData.UserNames[1]);
			await ChatService.Instance.EnterAsync(cxt, room, user_1);
			await ChatService.Instance.EnterAsync(cxt, room, user_2);
			// Act
			var newRoom = await ChatService
				.Instance
				.HighFiveAsync(cxt, room, user_1, user_2.Name);
			// Assert
			Assert.NotNull(newRoom);
			Assert.IsType<ChatRoom>(newRoom);
			Assert.True(newRoom.RealtimeLog.Last().SourceUserId == user_1.UserId);
			Assert.True(newRoom.RealtimeLog.Last().EventType == Enums.ChatEventEnum.HighFive);
		}
	}
}
