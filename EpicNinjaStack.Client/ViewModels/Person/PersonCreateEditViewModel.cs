using System;
using System.Threading.Tasks;
using EpicNinjaStack.DataAccess;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client.ViewModels.Person
{
	public class PersonCreateEditViewModel : BasePropertyChanged
	{
		private int? _id;
		private Domain.Person _person;

		private IAsyncCommand<bool> _save;
		private IAsyncCommand _load;

		public PersonCreateEditViewModel(int? id)
		{
			_id = id;

			Load = new AsyncCommand(LoadExecuteAsync, CanLoad);
			Save = new AsyncCommand<bool>(SaveExecuteAsync);
		}

		#region Commands

		public IAsyncCommand<bool> Save
		{
			get { return _save; }
			private set
			{
				if (Equals(value, _save)) return;
				_save = value;
				RaisePropertyChanged();
			}
		}

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

		#endregion Commands

		protected virtual IRepository<Domain.Person> Repository { get; set; }

		private void Close()
		{

		}

		private async Task SaveExecuteAsync(bool close = true)
		{
			_id = await Task.Run(() => Repository.Save(_person));

			if(close)
				Close();
		}

		private bool CanLoad()
		{
			return _id.HasValue;
		}

		private async Task LoadExecuteAsync()
		{
			if (!_id.HasValue)
				throw new InvalidOperationException("Cannot load without Id!");

			_person = await Task.Run(() => Repository.Get(_id.Value));
		}
	}
}
