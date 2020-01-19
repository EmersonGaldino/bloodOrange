using galdino.bloodOrange.api_v1.Models.Base;
using galdino.bloodOrange.api_v1.Models.Error;
using galdino.bloodOrange.api_v1.Results;
using galdino.bloodOrnage.application.core.Exception.Domain;
using Microsoft.AspNetCore.Mvc.Filters;

namespace galdino.bloodOrange.api_v1.Filters.Error
{
    public class ErrorFilters : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var coreError = context.Exception is DomainException;
            var mvcController = context.ActionDescriptor.RouteValues["controller"];
            var mvcAction = context.ActionDescriptor.RouteValues["action"];
            var errorMessage = $"{context.Exception.Message} {context.Exception.InnerException?.Message}";
            var result = new BaseViewModel<ErroViewModel>
            {

                Success = false,
                ObjectReturn = new ErroViewModel()
                {
                    Core = coreError,
                    Controller = mvcController,
                    Action = mvcAction,
                    Message = errorMessage
                },

            };
            context.Result = new InternalServerErrorObjectResult(result);
        }
    }
}
