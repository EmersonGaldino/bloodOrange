using galdino.bloodOrange.application.Interface.IUser;
using galdino.bloodOrange.application.Service.Base;
using galdino.bloodOrnage.application.core.Entity.User;
using galdino.bloodOrnage.application.core.Interface.IRepository.User;
using galdino.bloodOrnage.application.core.Interface.IService.User;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace galdino.bloodOrange.application.Service.User
{
	public class UserAppService : AppServiceBase<UserDomain, IUserService, IUserRepository>, IUserAppService
	{
		#region .::Constructor
		public UserAppService(IUserService userService) : base(userService)
		{

		}
		#endregion

		#region .::Methods
		public void ValidingTyperAccesUserAsync(ActionExecutingContext context)
		{

		}

		public async Task<UserDomain> ValidingUserAsync(UserDomain viewModel) =>
			await GetService().ValidingUserAsync(viewModel);
		#endregion

	}
}
