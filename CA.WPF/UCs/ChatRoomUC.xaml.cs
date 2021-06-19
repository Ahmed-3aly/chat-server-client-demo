namespace CA.UCs
{
	using CA.Enums;
	using CA.VMs;
	using System.Threading.Tasks;
	using System.Windows.Controls;
	using System.Windows.Media;
	public partial class ChatRoomUC :
		UserControl
	{
		public ChatRoomUC()
		{
			InitializeComponent();
			root.SelectionChanged += async (s, e) =>
			{
				if (DataContext == null)
				{
					return;
				}
				var vm = (ChatRoomVM)DataContext;
				vm.Resolution = A.IsSelected
					? ResolutionEnum.Realtime
					: ResolutionEnum.Hourly;
				await Task.Delay(100);
				ScrollContent(vm.Resolution == ResolutionEnum.Hourly);
			};
			DataContextChanged += (s, e) =>
				Refresh();
			SizeChanged += (s, e) =>
				Refresh();
		}
		void Refresh()
		{
			if (DataContext == null)
			{
				return;
			}
			var vm = (ChatRoomVM)DataContext;
			vm.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName.ToLower() == "RealtimeView".ToLower() ||
					e.PropertyName.ToLower() == "HourlyView".ToLower())
				{
					ScrollContent(vm.Resolution == ResolutionEnum.Hourly);
				};
			};
		}
		void ScrollContent(bool hourly)
		{
			if (hourly)
			{
				ScrollToBottom(A_Text);
			}
			else
			{
				ScrollToBottom(B_Text);
			}
		}
		void ScrollToBottom
		(
			TextBox v
		)
		{
			var c = VisualTreeHelper.GetChildrenCount(v);
			if (c < 1)
			{
				return;
			}
			var border = (Border)VisualTreeHelper.GetChild(v, 0);
			var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
			scrollViewer.ScrollToBottom();
		}
	}
}
