using AutoMapper;
using galdino.bloodOrange.api_v1.Controllers.Base;
using galdino.bloodOrange.api_v1.Models.Base;
using galdino.bloodOrange.api_v1.Models.Error;
using galdino.bloodOrange.api_v1.Models.ModelView.Launch;
using galdino.bloodOrange.api_v1.Models.Token;
using galdino.bloodOrange.api_v1.Models.ViewModel.Launch;
using galdino.bloodOrange.api_v1.Security;
using galdino.bloodOrange.application.Interface.IAccount;
using galdino.bloodOrange.application.Interface.ILogs;
using galdino.bloodOrange.application.shared.Confiugrations.Token;
using galdino.bloodOrange.application.shared.Interfaces.IMessages;
using galdino.bloodOrnage.application.core.Entity.Base.Account;
using galdino.bloodOrnage.application.core.Entity.Launch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace galdino.bloodOrange.api_v1.Controllers.Account
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    [EnableCors("SiteCorsPolicy")]
    [Produces("application/json")]
    public class AccountController : BaseController
    {
        #region .::Constructor
        private readonly IMapper mapper;
        private readonly ILogsAppService logsAppService;     

        public AccountController(IMapper mapper, ILogsAppService logsAppService)
        {
            this.mapper = mapper;
            this.logsAppService = logsAppService;
        }
        #endregion

        #region .::Methods
        /// <summary>
        /// Executa o debito na conta indicada, se houver saldo suficiente para tal.
        /// </summary>
        /// <response code="200">Debito em conta</response>
        /// <response code="400">Se os parametros do metodo estiverem nulos</response> 
        /// <response code="401">Acesso negado</response> 
        /// <response code="409">Se alguma regra de negocio for violada</response> 
        /// <response code="500">Erro interno desconhecido</response> 
        [HttpPost]
        [Route("LaunchDispense")]
        [ProducesResponseType(typeof(BaseViewModel<TokenViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> LaunchDispenseAccountAsync(
            [FromBody]LaunchViewModel accountModel,
            [FromServices] IAccountAppService accountService)
        {

            #region .::Mthod logs request headers
            logsAppService.SaveLog(this.HttpContext.Request.Headers); 
            #endregion

            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var model = mapper.Map<AccountLaunch>(accountModel);
           
            var search = await accountService.LaunchDispenseAccountAsync(model);

            var dataReturn = mapper.Map<LaunchModelView>(search);

            var returnModelView = new BaseViewModel<LaunchModelView>
            {
                Success = dataReturn.State,
                Message = dataReturn.Message,
                ObjectReturn = null

            };

            #region .::Method log result
            logsAppService.SaveLog(returnModelView); 
            #endregion

            return Ok(returnModelView);
        }
        #endregion
    }
}