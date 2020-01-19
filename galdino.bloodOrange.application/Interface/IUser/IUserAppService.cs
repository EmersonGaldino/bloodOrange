using galdino.bloodOrange.application.Interface.IBase;
using galdino.bloodOrnage.application.core.Entity.User;
using galdino.bloodOrnage.application.core.Interface.IRepository.User;
using galdino.bloodOrnage.application.core.Interface.IService.User;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace galdino.bloodOrange.application.Interface.IUser
{
	public interface IUserAppService : IAppServiceBase<UserDomain, IUserService, IUserRepository>
	{
		void ValidingTyperAccesUserAsync(ActionExecutingContext context);
		Task<UserDomain> ValidingUserAsync(UserDomain viewModel);
	}
}
