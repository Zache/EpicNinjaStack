using System.Threading.Tasks;

namespace EpicNinjaStack.MVVM
{
	public interface IEdit<T>
	{
		Task LoadAsync(int id);
	}
}