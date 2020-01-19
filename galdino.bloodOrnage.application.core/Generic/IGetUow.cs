using galdino.bloodOrange.application.shared.Interfaces.IUnitOfWork;

namespace galdino.bloodOrnage.application.core.Generic
{
    public interface IGetUow
    {
        IUnitOfWork GetUow();
    }
}
