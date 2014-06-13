using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private INavigator _navigator;

		protected override void OnStartup(StartupEventArgs e)
		{
			var container = new Container();
			container.RegisterDelegate<IRegistry>(_ => container, Reuse.Singleton);
			container.Register(typeof(INavigator), typeof(Navigator), Reuse.Singleton);
			var assembly = Assembly.GetExecutingAssembly();
			container.RegisterAll(assembly);

			_navigator = container.Resolve<INavigator>();

			base.OnStartup(e);
		}
	}
}
