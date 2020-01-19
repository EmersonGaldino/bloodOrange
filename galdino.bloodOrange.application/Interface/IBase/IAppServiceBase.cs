using galdino.bloodOrnage.application.core.Interface.IRepository.Base;
using galdino.bloodOrnage.application.core.Interface.IService.Base;

namespace galdino.bloodOrange.application.Interface.IBase
{
    public interface IAppServiceBase<T, S, R>
       where T : class
       where S : IServiceBase<T, R>
       where R : IRepositoryBase<T>
    {
        S GetService();
    }
}
