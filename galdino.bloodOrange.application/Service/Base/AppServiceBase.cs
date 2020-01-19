using galdino.bloodOrange.application.Interface.IBase;
using galdino.bloodOrnage.application.core.Interface.IRepository.Base;
using galdino.bloodOrnage.application.core.Interface.IService.Base;

namespace galdino.bloodOrange.application.Service.Base
{
    public class AppServiceBase<T, S, R> : IAppServiceBase<T, S, R>
          where T : class
          where S : IServiceBase<T, R>
          where R : IRepositoryBase<T>
    {
        protected readonly IServiceBase<T, R> _service;

        public AppServiceBase(IServiceBase<T, R> serviceBase)
        {
            _service = serviceBase;
        }

        public S GetService() => (S)_service;
    }
}
