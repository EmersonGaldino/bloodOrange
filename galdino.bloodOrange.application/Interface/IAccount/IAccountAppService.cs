using galdino.bloodOrange.application.Interface.IBase;
using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Interface.IRepository.Account;
using galdino.bloodOrnage.application.core.Interface.IService.Account;
using System.Threading.Tasks;

namespace galdino.bloodOrange.application.Interface.IAccount
{
    public interface IAccountAppService : IAppServiceBase<AccountModel, IAccountService, IAccountRepository>
    {
        Task<AccountLaunch> LaunchDispenseAccountAsync(AccountLaunch accountService);
    }
}
