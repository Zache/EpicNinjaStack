using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EpicNinjaStack.MVVM
{
	public interface IAsyncCommand<T> : ICommand
	{
		Task ExecuteAsync(T parameter);
	}
}
