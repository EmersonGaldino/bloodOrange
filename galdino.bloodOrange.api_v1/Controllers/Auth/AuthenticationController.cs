using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using galdino.bloodOrange.api_v1.Controllers.Base;
using galdino.bloodOrange.api_v1.Models.Base;
using galdino.bloodOrange.api_v1.Models.Error;
using galdino.bloodOrange.api_v1.Models.Token;
using galdino.bloodOrange.api_v1.Models.ViewModel.User;
using galdino.bloodOrange.api_v1.Security;
using galdino.bloodOrange.application.Interface.ILogs;
using galdino.bloodOrange.application.Interface.IUser;
using galdino.bloodOrange.application.shared.Confiugrations.Token;
using galdino.bloodOrange.application.shared.Interfaces.IMessages;
using galdino.bloodOrnage.application.core.Entity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace galdino.bloodOrange.api_v1.Controllers.Auth
{
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]")]
	[EnableCors("SiteCorsPolicy")]
	[Produces("application/json")]
	public class AuthenticationController : BaseController
	{
		#region .::Constructor
		private IMapper _mapper;
		private IMessaging message;
		private readonly ILogsAppService logsAppService;
		public AuthenticationController(IMapper mapper, IMessaging message, ILogsAppService logsAppService)
		{
			_mapper = mapper;
			this.message = message;
			this.logsAppService = logsAppService;
		}
		#endregion

		/// <summary>
		/// Retorna token de acesso ao usuario se o mesmo for validado corretamente
		/// </summary>
		/// <response code="200">Token de acesso a aplicacao</response>
		/// <response code="400">Se os parametros do metodo estiverem nulos</response> 
		/// <response code="401">Acesso negado</response> 
		/// <response code="409">Se alguma regra de negocio for violada</response> 
		/// <response code="500">Erro interno desconhecido</response> 
		[HttpPost]
		[Route("SignIn")]
		[ProducesResponseType(typeof(BaseViewModel<TokenViewModel>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
		[ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Login(
			[FromBody] UserViewModel usuario,
			[FromServices]TokenConfigurations tokenConfiguration,
			[FromServices]SignConfigurationToken signinConfiguration,
			[FromServices]IUserAppService usuarioAppService)
		{

			#region .::Method logs request
			logsAppService.SaveLog(this.HttpContext.Request.Headers); 
			#endregion

			if (usuario is null || !ModelState.IsValid)
				return BadRequest(ModelState);

			var objRetorno = new BaseViewModel<TokenViewModel>();

			var viewModel = _mapper.Map<UserDomain>(usuario);

			var userBase = await usuarioAppService.ValidingUserAsync(viewModel);
			if (userBase != null)
			{
				var identity = new ClaimsIdentity(
					new GenericIdentity(userBase.Login, "Login"),
					new[]
					{
						new Claim(JwtRegisteredClaimNames.Jti, userBase.UserId.ToString()),
						new Claim(JwtRegisteredClaimNames.UniqueName, userBase.Email)
					}
				);

				var dateCreate = DateTime.Now;
				var dateExpired = dateCreate + TimeSpan.FromDays(tokenConfiguration.ExpireIn);

				var handler = new JwtSecurityTokenHandler();

				var securityToken = handler.CreateToken(new SecurityTokenDescriptor
				{
					Issuer = tokenConfiguration.Issuer,
					Audience = tokenConfiguration.Audience,
					Subject = identity,
					NotBefore = dateCreate,
					Expires = dateExpired,

					SigningCredentials = new SigningCredentials(
						new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(tokenConfiguration.SigningKey)),
						SecurityAlgorithms.HmacSha256
					)
				});

				var token = handler.WriteToken(securityToken);
				objRetorno.Message = objRetorno.Success == true ? message.AUTH_SUCCESS() : message.AUTH_ERROR();
				objRetorno.ObjectReturn = new TokenViewModel()
				{
					UserId = userBase.UserId,
					Name = userBase.Name,
					Mail = userBase.Email,
					Authenticate = true,
					CreateAt = dateCreate,
					Expires = dateExpired,
					Token = token					

				};

				#region .::Methods Logs
				logsAppService.SaveLog(objRetorno); 
				#endregion

				return Ok(objRetorno);
			}

			return Unauthorized();
		}
	}
}