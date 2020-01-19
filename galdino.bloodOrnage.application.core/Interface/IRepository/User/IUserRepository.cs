using galdino.bloodOrnage.application.core.Entity.User;
using galdino.bloodOrnage.application.core.Interface.IRepository.Base;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Interface.IRepository.User
{
	public interface IUserRepository : IRepositoryBase<UserDomain>
	{
		Task<UserDomain> ValidingUserAsync(UserDomain viewModel);

	}
}
