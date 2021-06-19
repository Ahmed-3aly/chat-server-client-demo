namespace CA.UCs
{
	using CA.VMs;
	using System.Windows.Controls;
	public partial class MainUC :
		UserControl
	{
		public MainUC()
		{
			InitializeComponent();
			DataContext = MainVM.Instance;
			Loaded += async (s, e) =>
			{
				await MainVM.Instance.InitAsync();
			};
		}
	}
}
