using galdino.bloodOrange.application.shared.Interfaces.IConnections.BloodOrangeMongo;
using galdino.bloodOrange.data.persistence.Uow.Base;

namespace galdino.bloodOrange.data.persistence.Uow.BloodOrangeLogs
{
    public class UnitOfWorkBloodOrangeMongo : UnitOfWork, IConnectionBloodOrangeMongo
    {
        public UnitOfWorkBloodOrangeMongo(string connectionString) : base(connectionString)
        {

        }
    }
}
