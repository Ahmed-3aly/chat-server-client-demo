namespace CA.VMs
{
	using CA.Entities;
	public class RoomUserVM
	{
		public string Name { get; private set; }
		public bool IsSameUser { get; private set; }
		public DelegateCommand Greet { get; private set; }
		public DelegateCommand Highfive { get; private set; }
		public RoomUserVM
		(
			ChatRoomVM parent,
			ChatUser user,
			bool isSameUser
		)
		{
			IsSameUser = isSameUser;
			Name = user.Name;
			Greet = new DelegateCommand(async () =>
			{
				var _ = await ChatService.Instance.CommentAsync
				(
					AppDbContext.Factory(),
					parent.Room,
					parent.User,
					"Hi, " + Name
				);
				MainVM.Instance.Update(_);
			});
			Highfive = new DelegateCommand(async () =>
			{
				var _ = await ChatService.Instance.HighFiveAsync
				(
					AppDbContext.Factory(),
					parent.Room,
					parent.User,
					Name
				);
				MainVM.Instance.Update(_);
			});
		}
	}
}
