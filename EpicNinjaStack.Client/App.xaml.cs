using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using EpicNinjaStack.DataAccess;
using EpicNinjaStack.Domain;
using EpicNinjaStack.MVVM;

namespace EpicNinjaStack.Client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private INavigator _navigator;

		public static IRegistry Container;

		protected override void OnStartup(StartupEventArgs e)
		{
			var container = new Container();
			container.RegisterDelegate<IRegistry>(_ => container, Reuse.Singleton);
			container.Register(typeof(INavigator), typeof(Navigator), Reuse.Singleton);
			container.Register(typeof(IRepository<Person>), typeof(PersonRepository), Reuse.Singleton);
			var assembly = Assembly.GetExecutingAssembly();
			container.RegisterAll(assembly);

			Container = container;

			_navigator = container.Resolve<INavigator>();

			base.OnStartup(e);
		}
	}

	public class PersonRepository : IRepository<Person>
	{
		private readonly Dictionary<int, Person> _persons = new Dictionary<int, Person>
		{
			{ 1, new Person { Id = 1, Name = "Zacharias", DateOfBirth = new DateTime(1985,09,21), Gender = Gender.Yes, Email = "zacharias@tretton37.com"} }
		};

		public Task<Person> Get(int id)
		{
			return Task.FromResult(_persons[id]);
		}

		public Task<List<Person>> GetAll()
		{
			return Task.FromResult(_persons.Values.ToList());
		}

		public Task<int> Save(Person entity)
		{
			if (entity.Id.HasValue)
			{
				if (!_persons.ContainsKey(entity.Id.Value))
					_persons.Add(entity.Id.Value, entity);
				else
					_persons[entity.Id.Value] = entity;
			}
			else
			{
				var newId = _persons.Keys.Max() + 1;
				entity.Id = newId;
				_persons.Add(entity.Id.Value, entity);
			}

			return Task.FromResult(entity.Id.Value);
		}

		public Task Delete(int id)
		{
			if (_persons.ContainsKey(id))
				_persons.Remove(id);

			return Task.FromResult(true);
		}
	}
}
