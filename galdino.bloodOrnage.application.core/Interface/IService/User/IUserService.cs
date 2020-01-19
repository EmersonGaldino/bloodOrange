using galdino.bloodOrnage.application.core.Entity.User;
using galdino.bloodOrnage.application.core.Interface.IRepository.User;
using galdino.bloodOrnage.application.core.Interface.IService.Base;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Interface.IService.User
{
	public interface IUserService : IServiceBase<UserDomain, IUserRepository>
	{
		Task<UserDomain> ValidingUserAsync(UserDomain viewModel);
	}
}
