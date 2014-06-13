using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client.Views.Person
{
	/// <summary>
	/// Interaction logic for PersonCreateEditView.xaml
	/// </summary>
	public partial class PersonCreateEditView : IAdd<Domain.Person>, IEdit<Domain.Person>
	{
		private ICreateEditViewModel<Domain.Person> ViewModel
		{
			get { return DataContext as ICreateEditViewModel<Domain.Person>; }
			set { DataContext = value; }
		}

		public PersonCreateEditView(ICreateEditViewModel<Domain.Person> viewModel)
		{
			ViewModel = viewModel;
			
			InitializeComponent();
		}

		int? IAdd<Domain.Person>.Id
		{
			get { return ViewModel.Id; }
		}

		int IEdit<Domain.Person>.Id
		{
			get { return ViewModel.Id.Value; }
			set { ViewModel.Id = value; }
		}
	}
}
