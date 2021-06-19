namespace CA.VMs
{
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	public abstract class BindableBase :
		INotifyPropertyChanged
	{
		static protected string PropertyName
		(
			[CallerMemberName] string caller = null
		)
		{
			return caller;
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged
		(
			string propertyName
		)
		{
			PropertyChanged?.Invoke
			(
				this,
				new PropertyChangedEventArgs(propertyName)
			);
		}
		public void SetProperty<T>
		(
			ref T storage,
			T value,
			[CallerMemberName] string propertyName = null
		)
		{
			if (Equals(storage, value))
			{
				return;
			}
			storage = value;
			OnPropertyChanged(propertyName);
		}
	}
}
