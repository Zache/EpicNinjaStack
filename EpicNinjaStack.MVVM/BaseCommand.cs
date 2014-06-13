using System;
using System.Windows.Input;

namespace EpicNinjaStack.MVVM
{
	public abstract class BaseCommand : ICommand
	{
		public abstract bool CanExecute(object parameter = null);
		public abstract void Execute(object parameter = null);
		
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}