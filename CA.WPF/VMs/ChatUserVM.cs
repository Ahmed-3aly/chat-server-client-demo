namespace CA.VMs
{
	using CA.Entities;
	using System.Collections.Generic;
	public class ChatUserVM :
		BindableBase
	{
		public DelegateCommand Start { get; private set; }
		public MainVM Parent =>
			MainVM.Instance;
		public ChatUserVM
		(
			ChatUser user
		)
		{
			User = user;
			Rooms = new List<ChatRoomVM>
			{
				new ChatRoomVM(User),
			};
			Start = new DelegateCommand(() =>
				Connect()
			);
		}
		List<ChatRoomVM> _rooms;
		public List<ChatRoomVM> Rooms
		{
			get => _rooms;
			private set => SetProperty(ref _rooms, value);
		}
		ChatUser _user;
		public ChatUser User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		string _error;
		public string Error
		{
			get => _error;
			private set => SetProperty(ref _error, value);
		}
		bool _isConnected;
		public bool IsConnected
		{
			get => _isConnected;
			private set => SetProperty(ref _isConnected, value);
		}
		bool _hasError;
		public bool HasError
		{
			get => _hasError;
			private set => SetProperty(ref _hasError, value);
		}
		public void Connect()
		{
			IsConnected = true;
		}
	}
}
