using galdino.bloodOrnage.application.core.Entity.Logs;
using galdino.bloodOrnage.application.core.Interface.IService.Logs;
using galdino.bloodOrnage.application.core.Interface.Logs;
using galdino.bloodOrnage.application.core.Service.Base;
using Newtonsoft.Json;

namespace galdino.bloodOrnage.application.core.Service.Logs
{
    public class LogsService : ServiceBase<LogsModel, ILogsRepository>, ILogsService
    {
        #region .::Constructor
        public LogsService(ILogsRepository logRepository) : base(logRepository)
        {

        }

        #endregion


        #region .::Methods
        public  void SaveLog(object accountModel)
        {
            var convert = new LogsModel
            {
                Object = JsonConvert.SerializeObject(accountModel)
            };
            GetRepository().SaveLog(convert);
        }
             
         
        #endregion
    }
}
