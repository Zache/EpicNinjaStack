using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicNinjaStack.MVVM
{
	public class SyncCommand : BaseCommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public SyncCommand(Action execute) 
			: this(execute, () => true)
		{
		}

		public SyncCommand(Action execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public override bool CanExecute(object parameter = null)
		{
			return _canExecute();
		}

		public override void Execute(object parameter = null)
		{
			_execute();
		}
	}
}
