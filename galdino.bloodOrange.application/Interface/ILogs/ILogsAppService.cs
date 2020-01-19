using galdino.bloodOrange.application.Interface.IBase;
using galdino.bloodOrnage.application.core.Entity.Logs;
using galdino.bloodOrnage.application.core.Interface.IService.Logs;
using galdino.bloodOrnage.application.core.Interface.Logs;

namespace galdino.bloodOrange.application.Interface.ILogs
{
    public interface ILogsAppService : IAppServiceBase<LogsModel, ILogsService, ILogsRepository>
    {
        void SaveLog(object accountModel);
    }
}
