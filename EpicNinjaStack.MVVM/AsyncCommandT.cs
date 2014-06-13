using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpicNinjaStack.MVVM
{
	public class AsyncCommand<T> : BaseCommand
	{
		private readonly Func<T, Task> _execute;
		private readonly Func<bool> _canExecute;

		private bool _isExecuting;

		public AsyncCommand(Func<T, Task> execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public async Task ExecuteAsync(T parameter)
		{
			try
			{
				_isExecuting = true;

				await _execute(parameter);
			}
			finally
			{
				_isExecuting = false;
				CommandManager.InvalidateRequerySuggested();
			}
		}

		public override bool CanExecute(object parameter = null)
		{
			return !_isExecuting && _canExecute();
		}

		public async override void Execute(object parameter)
		{
			await ExecuteAsync((T)parameter);
		}
	}
}
