namespace CA.Tests
{
	using CA.Entities;
	using System;
	using Xunit;
	public class CreateRoomTests
	{
		[Theory]
		[MemberData(nameof(TestsData.RoomName_Invalid), MemberType = typeof(TestsData))]
		public async void CreateRoom_Throws_ArgumentException
		(
			string _
		)
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			// Assert
			await Assert.ThrowsAsync<ArgumentException>
			(
				// Act
				async () => await ChatService
					.Instance
					.CreateRoomAsync(cxt, _)
			);
		}
		[Theory]
		[MemberData(nameof(TestsData.RoomName_Valid), MemberType = typeof(TestsData))]
		public async void CreateRoom_Returns_ChatRoom
		(
			string _
		)
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			// Act
			var v = await ChatService
				.Instance
				.CreateRoomAsync(cxt, _);
			// Assert
			Assert.NotNull(v);
			Assert.IsType<ChatRoom>(v);
		}
	}
}
