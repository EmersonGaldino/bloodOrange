using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Interface.IRepository.Account;
using galdino.bloodOrnage.application.core.Interface.IService.Base;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Interface.IService.Account
{
    public interface IAccountService : IServiceBase<AccountModel, IAccountRepository>
    {        
        Task<AccountLaunch> LaunchDispenseAccountAsync(AccountLaunch accountLaunch);
    }
}
