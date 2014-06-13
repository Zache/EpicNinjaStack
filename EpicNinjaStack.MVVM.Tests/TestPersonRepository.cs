using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicNinjaStack.DataAccess;
using EpicNinjaStack.Domain;

namespace EpicNinjaStack.MVVM.Tests
{
	public class TestPersonRepository : IRepository<Person>
	{
		private readonly Dictionary<int, Person> _persons = new Dictionary<int, Person>();

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
