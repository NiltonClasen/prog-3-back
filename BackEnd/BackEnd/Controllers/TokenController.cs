using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Controllers
{
    [Route("RestAPIPesquisa/getToken")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        falta colocar os erros no swagger, os códigos, exemplo, tem o 200 lá, precisa colocar o 404 notfound
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] User request)
        {
            if (request.Login == "Admin" && request.Senha == "Admin")
            {
                var tokenString = GerarJSONWebToken(request);
                return Ok(new { token = tokenString });
            }
            return BadRequest();
        }

        private string GerarJSONWebToken(User usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var _claims = new List<Claim> { new Claim("usuarioLogin", usuario.Login.ToString()) };

            var token = new JwtSecurityToken(
                                    _configuration["Issuer"],
                                    _configuration["Audience"],
                                    claims: _claims,
                                    expires: DateTime.Now.AddHours(8),
                                    signingCredentials: creds
                                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}