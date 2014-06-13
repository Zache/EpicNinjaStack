using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using EpicNinjaStack.Client.ViewModels;

namespace EpicNinjaStack.MVVM
{
	public static class IocExtensions
	{
		public static Container RegisterAll(this Container container, Assembly assembly)
		{
			var types = from t in assembly.GetTypes()
							 from i in t.GetInterfaces()
							 where i.IsGenericType
							 where
							 i.GetGenericTypeDefinition() == typeof(ICreateEditViewModel<>)
							 || i.GetGenericTypeDefinition() == typeof(IListViewModel<>)
                             || i.GetGenericTypeDefinition() == typeof(IAdd<>)
							 || i.GetGenericTypeDefinition() == typeof(IEdit<>)
                             select new { Type = t, InterfaceType = i };

			foreach (var type in types)
				container.Register(type.InterfaceType, type.Type);

			return container;
		}

		public static Container RegisterViewModels(this Container container, Assembly assembly)
		{
			var viewModels = from t in assembly.GetTypes()
							 from i in t.GetInterfaces()
							 where i.IsGenericType
							 where 
							 i.GetGenericTypeDefinition() == typeof(ICreateEditViewModel<>)
							 || i.GetGenericTypeDefinition() == typeof(IListViewModel<>)
							 select new { ViewModelType = t, InterfaceType = i };

			foreach (var vm in viewModels)
				container.Register(vm.InterfaceType, vm.ViewModelType);

			return container;
		}

		public static Container RegisterViews(this Container container, Assembly assembly)
		{
			var views = from t in assembly.GetTypes()
						where t.BaseType == typeof(Window)
						from i in t.GetInterfaces()
						where i.IsGenericType
						where
						i.GetGenericTypeDefinition() == typeof(IAdd<>) 
						|| i.GetGenericTypeDefinition() == typeof(IEdit<>)
						select new { ViewType = t, InterfaceType = i };

			foreach (var view in views)
				container.Register(view.InterfaceType, view.ViewType);

			return container;
		}
	}
}
