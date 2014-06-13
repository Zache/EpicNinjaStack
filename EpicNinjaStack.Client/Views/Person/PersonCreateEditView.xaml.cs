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
using System.Windows.Shapes;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client.Views.Person
{
	/// <summary>
	/// Interaction logic for PersonCreateEditView.xaml
	/// </summary>
	public partial class PersonCreateEditView : IAdd<Domain.Person>, IEdit<Domain.Person>
	{
		public PersonCreateEditView()
		{
			InitializeComponent();
		}

		int? IAdd<Domain.Person>.Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		int IEdit<Domain.Person>.Id { get; set; }
	}
}
