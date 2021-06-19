namespace CA.Tests
{
	using CA.Entities;
	using System;
	using Xunit;
	public class CreateUserTests
	{
		[Theory]
		[MemberData(nameof(TestsData.UserName_Invalid), MemberType=typeof(TestsData))]
		public async void CreateUser_Throws_ArgumentException
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
					.CreateUserAsync(cxt, _)
			);
		}
		[Theory]
		[MemberData(nameof(TestsData.UserName_Valid), MemberType = typeof(TestsData))]
		public async void CreateUser_Returns_ChatUser
		(
			string _
		)
		{
			// Arrange
			using var cxt = TestDbContext.Factory();
			// Act
			var v = await ChatService
				.Instance
				.CreateUserAsync(cxt, _);
			// Assert
			Assert.NotNull(v);
			Assert.IsType<ChatUser>(v);
		}
	}
}
