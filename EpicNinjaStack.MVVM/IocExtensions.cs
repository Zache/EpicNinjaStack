using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;

namespace EpicNinjaStack.MVVM
{
	public static class IocExtensions
	{
		public static Container RegisterViews(this Container container, Assembly assembly)
		{
			var views = from t in assembly.GetTypes()
						where t.BaseType == typeof(Window)
						from i in t.GetInterfaces()
						where i == typeof(IAdd<>) || i == typeof(IEdit<>)
						select new { ViewType = t, InterfaceType = i };

			foreach (var view in views)
				container.Register(view.InterfaceType, view.ViewType);

			return container;
		}
	}
}
