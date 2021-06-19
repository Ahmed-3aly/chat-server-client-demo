namespace CA
{
	using CA.Entities;
	using CA.Interfaces;
	using Microsoft.EntityFrameworkCore;
	using System.Linq;
	using System.Threading.Tasks;
	public partial class ChatService :
		IChatService
	{
		public async Task<ChatUser> RestoreUserAsync
		(
			ChatDbContext cxt,
			string userName
		)
		{
			userName = userName.ValidateInputString();
			var find = await cxt
				.Users
				.Where(_ => _.Name.ToLower() == userName.ToLower())
				.SingleOrDefaultAsync();
			return find;
		}
	}
}
