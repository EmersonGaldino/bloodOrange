using Dapper;
using galdino.bloodOrange.application.shared.Interfaces.IConnections.BloodOrange;
using galdino.bloodOrange.data.persistence.Repository.Base;
using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Interface.IRepository.Account;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace galdino.bloodOrange.data.persistence.Repository.Account
{
    public class AccountRepository : RepositoryBase<AccountModel>, IAccountRepository
    {
        #region .::Constructor
        public IConnectionBloodOrange Uow { get; set; }
        public AccountRepository(IConnectionBloodOrange uow) : base(uow)
        {
            Uow = uow;
        }
        public IConnectionBloodOrange getUow() { return Uow; }

        #endregion


        #region .::Methods
        public async Task<AccountModel> GetAccountAsync(string accountLaunch)
        {
            try
            {
                var data = await Uow.GetConnection()
                    .QueryFirstOrDefaultAsync<AccountModel>(
                    $"SELECT {GetFields()} FROM {GetTableName()} WHERE AccountNumber = '{accountLaunch}'",
                    Uow.GetTransaction());

                return data;
            }
            finally { Uow.Release(); }
        }

        public async Task UpdateAccountsAsync(IList<AccountLaunch> accounts)
        {
            try
            {
                var query = new StringBuilder();
                foreach (var item in accounts)
                    query.AppendLine($"UPDATE accountuser SET dh_datechangeregistration = 'now()', accountbalance='{item.Value}' WHERE accountnumber ='{item.AccountCredit ?? item.AccountDebit}'; ");

                await Uow.GetConnection()
                     .QueryAsync(query.ToString());
            }
            finally { Uow.Release(); }
        }


        #endregion
    }

}
