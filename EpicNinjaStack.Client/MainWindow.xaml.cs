using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DryIoc;
using EpicNinjaStack.Client.ViewModels;
using EpicNinjaStack.Client.ViewModels.Person;
using EpicNinjaStack.Client.Views.Person;

namespace EpicNinjaStack.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			var listVm = App.Container.Resolve<IListViewModel<Domain.Person>>() as PersonListViewModel;
			var personList = new PersonListView(listVm);
			await listVm.Load.ExecuteAsync(null);

			Content = personList;
		}
	}
}
