using galdino.bloodOrnage.application.core.Generic;
using galdino.bloodOrnage.application.core.Interface.IRepository.Base;

namespace galdino.bloodOrnage.application.core.Interface.IService.Base
{
	public interface IServiceBase<T, R> : IGet<T>, ISelect<T> where T : class where R : IRepositoryBase<T>
	{
		R GetRepository();
	}
}
