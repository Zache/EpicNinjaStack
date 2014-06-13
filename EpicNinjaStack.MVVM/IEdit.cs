using System.Threading.Tasks;

namespace EpicNinjaStack.MVVM
{
	public interface IEdit<T>
	{
		object DataContext { get; }
		Task LoadAsync(int id);
	}
}