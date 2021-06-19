namespace CA.VMs
{
	using System;
	using System.Windows.Input;
	public class DelegateCommand :
		ICommand
	{
		Action _action;
		bool _isEnabled;
		public DelegateCommand(Action action)
		{
			_action = action;
			_isEnabled = true;
		}
		public void Execute(object parameter)
		{
			_action();
		}
		public bool CanExecute(object parameter)
		{
			return _isEnabled;
		}
		public bool IsEnabled
		{
			get => _isEnabled;
			set
			{
				_isEnabled = value;
				OnCanExecuteChanged();
			}
		}
		public event EventHandler CanExecuteChanged;
		protected virtual void OnCanExecuteChanged()
		{
			var handler = CanExecuteChanged;
			if (handler == null)
			{
				return;
			}
			handler(this, EventArgs.Empty);
		}
	}
}
