using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Generic
{
	public interface ISave<T> where T : class
	{
		Task SaveAsync(T entities);
	}
}
