using IDP.Application.Users.Comand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IDP.Api.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        public IActionResult Post(string UserName, string Password)
        {
            if (true)
            {
                var claims = new List<Claim>
                {
                    new Claim("UserId",Guid.NewGuid().ToString()),
                    new Claim("Name","elias.merati")
                };
                string key = _config["JwtConfig:key"];
                var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
                var credential = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(issuer: "elias merati"
                    , audience: "any"
                    , notBefore: DateTime.Now
                    , expires: DateTime.Now.AddMinutes(5)
                    , claims: claims
                    , signingCredentials: credential
                    );
                var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwttoken);
            }
        }

    }
}
