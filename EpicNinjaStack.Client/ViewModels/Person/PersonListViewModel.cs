using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EpicNinjaStack.DataAccess;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client.ViewModels.Person
{
	public class PersonListViewModel : BasePropertyChanged,IListViewModel<Domain.Person>
	{
		private IEnumerable<Domain.Person> _persons;
		private Domain.Person _selectedPerson;

		private IAsyncCommand _add;
		private IAsyncCommand _edit;
		private IAsyncCommand _remove;
		private IAsyncCommand _load;

		public PersonListViewModel(IRepository<Domain.Person> repository)
		{
			Repository = repository;

			Load = new AsyncCommand(LoadExecuteAsync);

			Add = new AsyncCommand(AddExecuteAsync);
			Edit = new AsyncCommand(EditExecuteAsync, CanEdit);
			Remove = new AsyncCommand(RemoveExecuteAsync, CanRemove);
		}

		#region Commands

		public IAsyncCommand Load
		{
			get { return _load; }
			private set
			{
				if (Equals(value, _load)) return;
				_load = value;
				RaisePropertyChanged();
			}
		}

		public IAsyncCommand Add
		{
			get { return _add; }
			private set
			{
				if (Equals(value, _add)) return;
				_add = value;
				RaisePropertyChanged();
			}
		}

		public IAsyncCommand Edit
		{
			get { return _edit; }
			private set
			{
				if (Equals(value, _edit)) return;
				_edit = value;
				RaisePropertyChanged();
			}
		}

		public IAsyncCommand Remove
		{
			get { return _remove; }
			private set
			{
				if (Equals(value, _remove)) return;
				_remove = value;
				RaisePropertyChanged();
			}
		}

		#endregion Commands

		public IEnumerable<Domain.Person> Items
		{
			get { return _persons; }
			private set
			{
				if (Equals(value, _persons)) return;
				_persons = value;
				RaisePropertyChanged();
			}
		}

		public Domain.Person SelectedItem
		{
			get { return _selectedPerson; }
			set
			{
				if (Equals(value, _selectedPerson)) return;
				_selectedPerson = value;
				RaisePropertyChanged();
			}
		}

		private IRepository<Domain.Person> Repository { get; set; }

		private async Task LoadExecuteAsync()
		{
			var selectedId = SelectedItem != null && SelectedItem.Id.HasValue 
				? (int?)SelectedItem.Id.Value 
				: null;

			var persons = await Task.Run(() => Repository.GetAll());
			Items = new ObservableCollection<Domain.Person>(persons);
			if (selectedId.HasValue && Items.Any())
				SelectedItem = Items.FirstOrDefault(p => p.Id == selectedId.Value) ?? Items.First();
		}

		private async Task AddExecuteAsync()
		{

		}

		private bool CanEdit()
		{
			return SelectedItem != null && SelectedItem.Id.HasValue;
		}

		private async Task EditExecuteAsync()
		{

		}

		private bool CanRemove()
		{
			return SelectedItem != null && SelectedItem.Id.HasValue;
		}

		private async Task RemoveExecuteAsync()
		{

		}
	}
}
