using galdino.bloodOrange.application.shared.Interfaces.IConnections.BloodOrange;
using galdino.bloodOrange.data.persistence.Uow.Base;

namespace galdino.bloodOrange.data.persistence.Uow.bloodOrange
{
    public class UnitOfWorkBloodOrange : UnitOfWork, IConnectionBloodOrange
    {
        public UnitOfWorkBloodOrange(string connectionString): base(connectionString)
        {

        }
    }
}
