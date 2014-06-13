using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicNinjaStack.Client.ViewModels
{
	public interface IListViewModel<T> : INotifyPropertyChanged
	{
		IEnumerable<T> Items { get; }
		T SelectedItem { get; set; }
	}
}
