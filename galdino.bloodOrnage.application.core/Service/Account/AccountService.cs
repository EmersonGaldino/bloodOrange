using galdino.bloodOrange.application.shared.Interfaces.IMessages;
using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Interface.IRepository.Account;
using galdino.bloodOrnage.application.core.Interface.IService.Account;
using galdino.bloodOrnage.application.core.Interface.IService.Logs;
using galdino.bloodOrnage.application.core.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Service.Account
{
    public class AccountService : ServiceBase<AccountModel, IAccountRepository>, IAccountService
    {
        #region .::Constructor
        private readonly IMessaging messages;
        private readonly ILogsService logsService;
        public AccountService(IAccountRepository accountRepository, IMessaging messages, ILogsService logsService ) : base(accountRepository)
        {
            this.messages = messages;
            this.logsService = logsService;
        }


        #endregion

        #region .::Methods
        public async Task<AccountModel> GetAccountAsync(string accountLaunch) =>
             await GetRepository().GetAccountAsync(accountLaunch);

        public async Task UpdateAccountsAsync(IList<AccountLaunch> accounts) =>
            await GetRepository().UpdateAccountsAsync(accounts);

        public async Task<AccountLaunch> LaunchDispenseAccountAsync(AccountLaunch accountLaunch)
        {
            var accountCredit = await GetAccountAsync(accountLaunch.AccountCredit);
            var accountDebit = await GetAccountAsync(accountLaunch.AccountDebit);

            if (accountDebit == null)
                return new AccountLaunch { Message = messages.ACCOUNT_DEBIT_NOT_EXISTS(), State = false };


            if (accountLaunch.Value > accountDebit.CreditInCash)
                return new AccountLaunch { Message = messages.ACCOUNT_FOUNDS_INSUFFICIENT(), State = false };

            if (accountCredit == null)
                return new AccountLaunch { Message = messages.ACCOUNT_NOT_FOUND(), State = false };

            var list = new List<AccountLaunch>
            {
                new AccountLaunch
                {
                    AccountCredit = accountCredit.Account,
                    Value = (accountCredit.CreditInCash + accountLaunch.Value),
                    State = true,
                    Debit = false
                },
                new AccountLaunch
                {
                    AccountDebit = accountDebit.Account,
                    Value = (accountDebit.CreditInCash - accountLaunch.Value),
                    State = true,
                    Debit = true
                }
            };
            await UpdateAccountsAsync(list);

            logsService.SaveLog(list);

            return new AccountLaunch { Message= messages.DEBIT_MADE_SUCCESS(), State = true};
        }
        #endregion
    }
}
