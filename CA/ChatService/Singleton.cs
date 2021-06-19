namespace CA
{
	using CA.Interfaces;
	public partial class ChatService :
		IChatService
	{
		static public readonly IChatService Instance =
			new ChatService();
		ChatService() { }
	}
}
