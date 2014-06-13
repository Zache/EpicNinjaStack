namespace EpicNinjaStack.MVVM
{
	public interface INavigator
	{
		int? Add<T>();
		bool Edit<T>(int id);
	}
}