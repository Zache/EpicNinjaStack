using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicNinjaStack.DataAccess
{
	public interface IRepository<T>
	{
		Task<T> Get(int id);
		Task<List<T>> GetAll();
		Task<int> Save(T entity);
		Task Delete(int id);
	}
}
