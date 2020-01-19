using galdino.bloodOrange.application.Interface.IAccount;
using galdino.bloodOrange.application.Service.Base;
using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Interface.IRepository.Account;
using galdino.bloodOrnage.application.core.Interface.IService.Account;
using System.Threading.Tasks;

namespace galdino.bloodOrange.application.Service.Account
{
    public class AccountAppService : AppServiceBase<AccountModel, IAccountService, IAccountRepository>, IAccountAppService
    {
        #region .::Constructor
        public AccountAppService(IAccountService appService) : base(appService)
        {

        }
        #endregion

        #region .::Methods
        public async Task<AccountLaunch> LaunchDispenseAccountAsync(AccountLaunch accountLaunch) =>
          await GetService().LaunchDispenseAccountAsync(accountLaunch); 
        #endregion


    }
}
