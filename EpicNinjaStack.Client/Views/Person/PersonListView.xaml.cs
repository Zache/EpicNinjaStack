using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EpicNinjaStack.Client.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace EpicNinjaStack.Client.Views.Person
{
	/// <summary>
	/// Interaction logic for PersonListView.xaml
	/// </summary>
	public partial class PersonListView : UserControl
	{
		private IListViewModel<Domain.Person> ViewModel
		{
			get { return DataContext as IListViewModel<Domain.Person>; }
			set { DataContext = value; }
		}

		public PersonListView(IListViewModel<Domain.Person> viewModel)
		{
			ViewModel = viewModel;

			InitializeComponent();
		}
	}
}
