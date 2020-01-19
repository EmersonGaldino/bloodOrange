using galdino.bloodOrange.application.Interface.IUser;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace galdino.bloodOrange.api_v1.Filters.Security
{
    public class SecurityFilter : IAsyncActionFilter
    {
        private readonly IUserAppService _usuarioAppService;
        public SecurityFilter(

            IUserAppService usuarioAppService)
        {

            _usuarioAppService = usuarioAppService;
        }
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                _usuarioAppService.ValidingTyperAccesUserAsync(context);
            }
            return next();
        }
    }
}
