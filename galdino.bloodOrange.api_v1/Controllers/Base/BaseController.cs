using galdino.bloodOrnage.application.core.Entity.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace galdino.bloodOrange.api_v1.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public int UsuarioId => Convert.ToInt32(User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value);
        public string UsuarioNome => User.Identity.Name;
        public string IP => Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();       

        public UserDomain Usuario => new UserDomain
        {
            UserId = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value,           

        };
    }
}
