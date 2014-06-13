using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;

namespace EpicNinjaStack.MVVM
{
	public class Navigator : INavigator
	{
		private readonly IRegistry _iocContainer;

		public Navigator(IRegistry iocContainer)
		{
			_iocContainer = iocContainer;
		}

		public async Task<int?> AddAsync<T>()
		{
			var view = _iocContainer.Resolve<IAdd<T>>();
			await view.LoadAsync();
			var window = view as Window;
			if(window == null)
				throw new Exception("View that was not a window!!");
			window.ShowDialog();
			return view.Id;
		}

		public async Task<bool> EditAsync<T>(int id)
		{
			var view = _iocContainer.Resolve<IEdit<T>>();
			await view.LoadAsync(id);
			var window = view as Window;
			if (window == null)
				throw new Exception("View that was not a window!!");
			var dialogResult = window.ShowDialog();

			return dialogResult.HasValue && dialogResult.Value;
		}

		public void Close(object caller, bool dialogResult)
		{
			throw new NotImplementedException();
		}
	}
}
