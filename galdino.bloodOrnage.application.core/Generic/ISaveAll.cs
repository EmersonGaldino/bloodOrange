using System.Collections.Generic;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Generic
{
	public interface ISaveAll<T> where T : class
	{
		Task SaveAsync(IList<T> entities);
	}
}
