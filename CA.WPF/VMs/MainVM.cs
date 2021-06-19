namespace CA.VMs
{
	using CA.Entities;
	using CA.Exceptions;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	public class MainVM :
		BindableBase
	{
		static public readonly MainVM Instance =
			new();
		List<ChatUserVM> _users;
		public List<ChatUserVM> Users
		{
			get => _users;
			private set => SetProperty(ref _users, value);
		}
		ChatRoom _room;
		public ChatRoom Room
		{
			get => _room;
			private set => SetProperty(ref _room, value);
		}
		public void Update
		(
			ChatRoom room
		)
		{
			Room = room;
			Users.ForEach(_ =>
			{
				_.Rooms.ForEach(room =>
				{
					room.Update();
				});
			});
		}
		MainVM()
		{
		}
		public async Task InitAsync()
		{
			using var cxt = AppDbContext.Factory();
			Room = await ChatService.Instance.CreateRoomAsync
			(
				cxt,
				"#general"
			);
			var names = new List<string>
			{
				"Alice",
				"Bob",
				"Eve",
			};
			var users = new List<ChatUserVM>();
			names.ForEach(async _ =>
			{
				var user = (ChatUser)null;
				var restoreConnection = false;
				try
				{
					user = await ChatService
						.Instance
						.CreateUserAsync(cxt, _);
				}
				catch (DuplicateUserNameException)
				{
					restoreConnection = true;
				}
				if (restoreConnection)
				{
					user = await ChatService
						.Instance
						.RestoreUserAsync(cxt, _);
				}
				users.Add(new ChatUserVM(user));
			});
			Users = users;
			Update(Room);
		}
	}
}
