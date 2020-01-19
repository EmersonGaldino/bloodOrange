using galdino.bloodOrnage.application.core.Entity.User;
using galdino.bloodOrnage.application.core.Interface.IRepository.User;
using galdino.bloodOrnage.application.core.Interface.IService.User;
using galdino.bloodOrnage.application.core.Service.Base;
using System.Threading.Tasks;

namespace galdino.bloodOrnage.application.core.Service.User
{
	public class UserService : ServiceBase<UserDomain, IUserRepository>, IUserService
	{
		#region .::Constructor
		public UserService(IUserRepository userRepository) : base(userRepository)
		{

		}
		#endregion

		#region .::Methods
		public async Task<UserDomain> ValidingUserAsync(UserDomain viewModel) =>
			await GetRepository().ValidingUserAsync(viewModel);

		#endregion
	}
}
