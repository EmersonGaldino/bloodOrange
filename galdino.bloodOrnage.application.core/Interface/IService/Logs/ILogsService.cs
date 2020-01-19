using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Entity.Logs;
using galdino.bloodOrnage.application.core.Interface.IService.Base;
using galdino.bloodOrnage.application.core.Interface.Logs;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Interface.IService.Logs
{
    public interface ILogsService : IServiceBase<LogsModel, ILogsRepository>
    {
        void SaveLog(object accountModel);
    }
}
