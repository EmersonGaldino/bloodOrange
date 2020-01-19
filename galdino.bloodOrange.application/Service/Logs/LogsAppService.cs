using galdino.bloodOrange.application.Interface.ILogs;
using galdino.bloodOrange.application.Service.Base;
using galdino.bloodOrnage.application.core.Entity.Logs;
using galdino.bloodOrnage.application.core.Interface.IService.Logs;
using galdino.bloodOrnage.application.core.Interface.Logs;
using Newtonsoft.Json;

namespace galdino.bloodOrange.application.Service.Logs
{
    public class LogsAppService : AppServiceBase<LogsModel, ILogsService, ILogsRepository>, ILogsAppService
    {
        #region .::Constructor
        public LogsAppService(ILogsService appService) : base(appService)
        {

        }
        #endregion

        #region .::Methods

        public void SaveLog(object accountModel) =>
            GetService().SaveLog(accountModel);


        #endregion

    }
}
