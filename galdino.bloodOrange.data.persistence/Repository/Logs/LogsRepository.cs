using Dapper;
using galdino.bloodOrange.application.shared.Interfaces.IConnections.BloodOrange;
using galdino.bloodOrange.data.persistence.Repository.Base;
using galdino.bloodOrnage.application.core.Entity.Logs;
using galdino.bloodOrnage.application.core.Interface.Logs;
using System.Threading.Tasks;

namespace galdino.bloodOrange.data.persistence.Repository.Logs
{
    public class LogsRepository : RepositoryBase<LogsModel>, ILogsRepository
    {
        #region .::Constructor
        public IConnectionBloodOrange Uow { get; set; }

        public LogsRepository(IConnectionBloodOrange uow) : base(uow)
        {
            Uow = uow;

        }
        public IConnectionBloodOrange getUow() { return Uow; }
        #endregion


        public void SaveLog(LogsModel accountModel)
        {
            try
            {
                Uow.GetConnection().Execute($"INSERT INTO logsAccount VALUES('{accountModel.Id}','{accountModel.Object}')", Uow.GetTransaction());
            }
            finally { Uow.Release(); }
        }
    }
}
