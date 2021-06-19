namespace CA.VMs
{
	using CA.Entities;
	using CA.Enums;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	public class ChatRoomVM :
		BindableBase
	{
		public DelegateCommand Enter { get; private set; }
		public DelegateCommand Leave { get; private set; }
		public bool IsConnected =>
			Room != null &&
			Room.UsersLog != null &&
			Room.UsersLog.Any(x => x.ChatRoomUser_UserId == User.UserId);
		public ChatRoom Room =>
			MainVM.Instance.Room;
		bool _isRealtime;
		public bool IsRealtime
		{
			get => _isRealtime;
			private set => SetProperty(ref _isRealtime, value);
		}
		ResolutionEnum _resolution;
		public ResolutionEnum Resolution
		{
			get => _resolution;
			set
			{
				SetProperty(ref _resolution, value);
				IsRealtime = Resolution == ResolutionEnum.Realtime;
			}
		}
		string _hourlyView;
		public string HourlyView
		{
			get => _hourlyView;
			private set => SetProperty(ref _hourlyView, value);
		}
		string _realtimeView;
		public string RealtimeView
		{
			get => _realtimeView;
			private set => SetProperty(ref _realtimeView, value);
		}
		List<RoomUserVM> _users;
		public List<RoomUserVM> Users
		{
			get => _users;
			private set => SetProperty(ref _users, value);
		}
		ChatUser _user;
		public ChatUser User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		public ChatRoomVM
		(
			ChatUser user
		)
		{
			User = user;
			Resolution = ResolutionEnum.Realtime;
			Enter = new DelegateCommand(async () => await ToggleAsync(false));
			Leave = new DelegateCommand(async () => await ToggleAsync(true));
		}
		async Task ToggleAsync
		(
			bool leave
		)
		{
			using var cxt = AppDbContext.Factory();
			if (leave)
			{
				var _ = await ChatService.Instance.LeaveAsync
				(
					cxt,
					Room,
					User
				);
				MainVM.Instance.Update(_);
			}
			else
			{
				var _ = await ChatService.Instance.EnterAsync
				(
					cxt,
					Room,
					User
				);
				MainVM.Instance.Update(_);
			}
			Update();
		}
		public void Update()
		{
			OnPropertyChanged("IsConnected");
			if (!IsConnected)
			{
				Users = null;
				HourlyView = null;
				RealtimeView = null;
				return;
			}
			Users = Room
				.UsersLog
				.OrderBy(_ => _.ChatRoomUser_User.Name)
				.Select(_ => new RoomUserVM(this, _.ChatRoomUser_User, _.ChatRoomUser_User.UserId == User.UserId))
				.ToList();
			HourlyView = string.Join(Environment.NewLine, Room.HourlyLog.Select(_ => _.Print));
			RealtimeView = string.Join(Environment.NewLine, Room.RealtimeLog.OrderBy(_ => _.On).Select(_ => _.Print));
		}
	}
}
