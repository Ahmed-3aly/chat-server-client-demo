namespace CA
{
	using CA.Entities;
	using CA.Exceptions;
	using CA.Interfaces;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	public partial class ChatService :
		IChatService
	{
		public async Task<ChatUser> CreateUserAsync
		(
			ChatDbContext cxt,
			string userName
		)
		{
			if (cxt == null)
			{
				throw new ArgumentException();
			}
			userName = userName.ValidateInputString();
			var find = await cxt
				.Users
				.Where(_ => _.Name.ToLower() == userName.ToLower())
				.SingleOrDefaultAsync();
			if (find != null)
			{
				throw new DuplicateUserNameException();
			}
			var append = new ChatUser
			{
				Name = userName,
			};
			await cxt
				.Users
				.AddAsync(append);
			await cxt.SaveChangesAsync();
			return append;
		}
	}
}
