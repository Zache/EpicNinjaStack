using System;
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
		private ObservableCollection<Domain.Person> _persons;
		private Domain.Person _selectedPerson;

		private IAsyncCommand _add;
		private IAsyncCommand _edit;
		private IAsyncCommand _remove;
		private IAsyncCommand<int?> _load;
		
		public PersonListViewModel(INavigator navigator, IRepository<Domain.Person> repository)
		{
			Navigator = navigator;
			Repository = repository;
			
			Load = new AsyncCommand<int?>(LoadExecuteAsync);

			Add = new AsyncCommand(AddExecuteAsync);
			Edit = new AsyncCommand(EditExecuteAsync, CanEdit);
			Remove = new AsyncCommand(RemoveExecuteAsync, CanRemove);
		}

		#region Commands

		public IAsyncCommand<int?> Load
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

		public ObservableCollection<Domain.Person> Items
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
		private INavigator Navigator { get; set; }

		private async Task LoadExecuteAsync(int? selectId = null)
		{
			var selectedId = selectId.HasValue 
				? selectId.Value
				: SelectedItem != null && SelectedItem.Id.HasValue 
					? (int?)SelectedItem.Id.Value 
					: null;
			
			var persons = await Task.Run(() => Repository.GetAll());
			Items = new ObservableCollection<Domain.Person>(persons);
			if (selectedId.HasValue && Items.Any())
				SelectedItem = Items.FirstOrDefault(p => p.Id == selectedId.Value) ?? Items.First();
		}

		private async Task AddExecuteAsync()
		{
			var newId = await Navigator.AddAsync<Domain.Person>();
			if (newId.HasValue)
				await Load.ExecuteAsync(newId.Value);
		}

		private bool CanEdit()
		{
			return SelectedItem != null && SelectedItem.Id.HasValue;
		}

		private async Task EditExecuteAsync()
		{
			if(SelectedItem== null || !SelectedItem.Id.HasValue)
				throw new InvalidOperationException("Must have a selected item with an Id to edit!");

			var edited = await Navigator.EditAsync<Domain.Person>(SelectedItem.Id.Value);
			if (edited)
				await Load.ExecuteAsync(SelectedItem.Id.Value);
		}

		private bool CanRemove()
		{
			return SelectedItem != null && SelectedItem.Id.HasValue;
		}

		private async Task RemoveExecuteAsync()
		{
			if (SelectedItem == null || !SelectedItem.Id.HasValue)
				throw new InvalidOperationException("Must have a selected item with an Id to delete!");

			await Repository.Delete(SelectedItem.Id.Value);
			await Load.ExecuteAsync(null);
		}
	}
}
