using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpicNinjaStack.MVVM
{
	public class AsyncCommand : BaseCommand, IAsyncCommand
	{
		private readonly Func<Task> _execute;
		private readonly Func<bool> _canExecute;

		private bool _isExecuting;

		public AsyncCommand(Func<Task> execute)
			: this(execute, () => true)
		{
		}

		public AsyncCommand(Func<Task> execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public async Task ExecuteAsync(object parameter = null)
		{
			try
			{
				_isExecuting = true;

				await _execute();
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

		public async override void Execute(object parameter = null)
		{
			await ExecuteAsync(parameter);
		}
	}
}