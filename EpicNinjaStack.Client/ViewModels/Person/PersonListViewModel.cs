using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EpicNinjaStack.DataAccess;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client.ViewModels.Person
{
	public class PersonListViewModel : BasePropertyChanged
	{
		private ObservableCollection<Domain.Person> _persons;
		private Domain.Person _selectedPerson;

		private IAsyncCommand _add;
		private IAsyncCommand _edit;
		private IAsyncCommand _remove;
		private IAsyncCommand _load;

		public PersonListViewModel()
		{ 
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

		public ObservableCollection<Domain.Person> Persons
		{
			get { return _persons; }
			private set
			{
				if (Equals(value, _persons)) return;
				_persons = value;
				RaisePropertyChanged();
			}
		}

		public Domain.Person SelectedPerson
		{
			get { return _selectedPerson; }
			set
			{
				if (Equals(value, _selectedPerson)) return;
				_selectedPerson = value;
				RaisePropertyChanged();
			}
		}

		protected virtual IRepository<Domain.Person> Repository { get; set; }

		private async Task LoadExecuteAsync()
		{
			var selectedId = SelectedPerson != null && SelectedPerson.Id.HasValue 
				? (int?)SelectedPerson.Id.Value 
				: null;

			var persons = await Task.Run(() => Repository.GetAll());
			Persons = new ObservableCollection<Domain.Person>(persons);
			if (selectedId.HasValue && Persons.Count > 0)
				SelectedPerson = Persons.FirstOrDefault(p => p.Id == selectedId.Value) ?? Persons.First();
		}

		private async Task AddExecuteAsync()
		{

		}

		private bool CanEdit()
		{
			return SelectedPerson != null && SelectedPerson.Id.HasValue;
		}

		private async Task EditExecuteAsync()
		{

		}

		private bool CanRemove()
		{
			return SelectedPerson != null && SelectedPerson.Id.HasValue;
		}

		private async Task RemoveExecuteAsync()
		{

		}
	}
}
