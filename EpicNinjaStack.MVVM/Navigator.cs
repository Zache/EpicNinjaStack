using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public int? Add<T>()
		{
			throw new NotImplementedException();
		}

		public bool Edit<T>(int id)
		{
			throw new NotImplementedException();
		}
	}
}
