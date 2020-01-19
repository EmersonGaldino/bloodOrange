using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Interface.IRepository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Interface.IRepository.Account
{
    public interface IAccountRepository : IRepositoryBase<AccountModel>
    {
       
        Task<AccountModel> GetAccountAsync(string accountLaunch);
        Task UpdateAccountsAsync(IList<AccountLaunch> accounts);
    }
}
