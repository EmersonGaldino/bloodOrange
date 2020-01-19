using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Entity.Logs;
using galdino.bloodOrnage.application.core.Interface.IRepository.Base;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Interface.Logs
{
    public interface ILogsRepository : IRepositoryBase<LogsModel>
    {
        void SaveLog(LogsModel accountModel);
    }
}
