namespace CA
{
	using CA.Entities;
	using CA.Interfaces;
	using System.Threading.Tasks;
	public partial class ChatService :
		IChatService
	{
		internal async Task<ChatRoom> RaiseAsync
		(
			ChatDbContext cxt,
			string roomName,
			ChatEvent e
		)
		{
			await cxt.EventsLog.AddAsync(e);
			await cxt.SaveChangesAsync();
			// send updated model back to all client viewmodels
			return await CreateRoomAsync(cxt, roomName);
		}
	}
}
