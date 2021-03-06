﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using EpicNinjaStack.DataAccess;
using EpicNinjaStack.Domain;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client.ViewModels.Person
{
	public class PersonCreateEditViewModel : BasePropertyChanged, ICreateEditViewModel<Domain.Person>
	{
		private Domain.Person _person;

		private IAsyncCommand<bool> _save;
		private IAsyncCommand<int?> _load;
		private ICommand _cancel;
		private ICommand _undo;

		private string _name;
		private string _email;
		private DateTime _dateOfBirth;
		private Gender _gender;

		private bool _isDirty;

		static PersonCreateEditViewModel()
		{
			Mapper.CreateMap<Domain.Person, PersonCreateEditViewModel>();
			Mapper.CreateMap<PersonCreateEditViewModel, Domain.Person>();
		}

		public PersonCreateEditViewModel(INavigator navigator, IRepository<Domain.Person> repository)
		{
			Navigator = navigator;
			Repository = repository;

			Load = new AsyncCommand<int?>(LoadExecuteAsync, CanLoad);
			Save = new AsyncCommand<bool>(SaveExecuteAsync, CanSave);

			Cancel = new SyncCommand(CancelExecute);
			Undo = new SyncCommand(UndoExecute, CanUndo);
		}

		public int? Id
		{
			get { return _person.Id; }
		}

		public string Name
		{
			get { return _name; }
			set
			{
				if (value == _name) return;
				_name = value;
				RaisePropertyChanged();
			}
		}

		public string Email
		{
			get { return _email; }
			set
			{
				if (value == _email) return;
				_email = value;
				RaisePropertyChanged();
			}
		}

		public DateTime DateOfBirth
		{
			get { return _dateOfBirth; }
			set
			{
				if (value.Equals(_dateOfBirth)) return;
				_dateOfBirth = value;
				RaisePropertyChanged();
			}
		}

		public Gender Gender
		{
			get { return _gender; }
			set
			{
				if (value == _gender) return;
				_gender = value;
				RaisePropertyChanged();
			}
		}

		#region Commands

		public IAsyncCommand<bool> Save
		{
			get { return _save; }
			private set
			{
				if (Equals(value, _save)) return;
				_save = value;
				RaisePropertyChanged(false);
			}
		}

		public ICommand Cancel
		{
			get { return _cancel; }
			private set
			{
				if (Equals(value, _cancel)) return;
				_cancel = value;
				RaisePropertyChanged(false);
			}
		}

		public IAsyncCommand<int?> Load
		{
			get { return _load; }
			private set
			{
				if (Equals(value, _load)) return;
				_load = value;
				RaisePropertyChanged(false);
			}
		}

		public ICommand Undo
		{
			get { return _undo; }
			private set
			{
				if (Equals(value, _undo)) return;
				_undo = value;
				RaisePropertyChanged(false);
			}
		}

		#endregion Commands

		public bool IsDirty
		{
			get { return _isDirty && _person != null; }
			private set
			{
				if (value.Equals(_isDirty)) return;
				_isDirty = value;
				RaisePropertyChanged(false);
			}
		}

		private IRepository<Domain.Person> Repository { get; set; }
		private INavigator Navigator { get; set; }

		private void Close(bool dialogResult)
		{
			Navigator.Close(this, dialogResult);
		}

		private bool CanSave()
		{
			return IsDirty;
		}

		private async Task SaveExecuteAsync(bool close = true)
		{
			EndEdit();
			await Task.Run(() => Repository.Save(_person));

			if (close)
				Close(true);
		}

		private bool CanLoad()
		{
			return Id.HasValue;
		}

		private async Task LoadExecuteAsync(int? id)
		{
			if (id.HasValue)
				_person = await Task.Run(() => Repository.Get(id.Value));
			else
				_person = new Domain.Person
				{
					DateOfBirth = DateTime.UtcNow,
					Gender = Gender.Maybe
				};
			BeginEdit();
		}

		private void CancelExecute()
		{
			CancelEdit();
			Close(false);
		}

		private bool CanUndo()
		{
			return IsDirty;
		}

		private void UndoExecute()
		{
			CancelEdit();
		}

		public void BeginEdit()
		{
			Mapper.Map(_person, this);
			IsDirty = false;
		}

		public void EndEdit()
		{
			Mapper.Map(this, _person);
			IsDirty = false;
		}

		public void CancelEdit()
		{
			Mapper.Map(_person, this);
			IsDirty = false;
		}

		protected override void RaisePropertyChanged(string propertyName = null)
		{
			IsDirty = true;

			base.RaisePropertyChanged(propertyName);
		}

		private void RaisePropertyChanged(bool isDirty, string propertyName = null)
		{
			if (isDirty)
				IsDirty = true;

			base.RaisePropertyChanged(propertyName);
		}
	}
}
