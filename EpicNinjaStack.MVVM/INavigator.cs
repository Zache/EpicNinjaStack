using System.Threading.Tasks;

namespace EpicNinjaStack.MVVM
{
	public interface INavigator
	{
		Task<int?> AddAsync<T>();
		Task<bool> EditAsync<T>(int id);
		void Close(object caller, bool dialogResult);
	}
}