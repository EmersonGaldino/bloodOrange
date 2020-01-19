using galdino.bloodOrnage.application.core.Generic;

namespace galdino.bloodOrnage.application.core.Interface.IRepository.Base
{
	public interface IRepositoryBase<T> : IGet<T>,
		 ISelect<T>, IListByIds<T>, IGetUow, ISave<T>, ISaveAll<T> where T : class
	{
	}
}
