using Dapper;
using IDP.Application.Users.Comand;
using IDP.Common.Security;
using IDP.Core.Entities;
using IDP.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IDP.Api.Controllers
{
#nullable disable
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDPContext _db;
        public AccountController(IConfiguration config , IDPContext db)
        {
            _config = config;
            _db = db;
        }
        [HttpPost]
        public IActionResult Post(string UserName, string Password , int Id)
        {
            var user = _db.Users.FirstOrDefault(u=> u.UserId == Id);
            bool existUser = _db.Users.Any(u=>u.UserName == UserName && u.Password == Password && u.IsActive);

            if (existUser)
            {
                var claims = new List<Claim>
                {
                    new Claim("UserId",user.UserId.ToString()),
                    new Claim("Name",user.FullName)
                };
                var expiretoken = DateTime.Now.AddMinutes(int.Parse(_config["JwtConfig:expires"]));
                string key = _config["JwtConfig:key"];
                var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
                var credential = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(issuer: _config["JwtConfig:issuer"]
                    , audience: _config["JwtConfig:audience"]
                    , notBefore: DateTime.Now
                    , expires: expiretoken
                    , claims: claims
                    , signingCredentials: credential
                    );
                var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
                var userToken = new UserToken()
                {
                    Token = HashHelper.CreateHash(jwttoken),
                    TokenExpire = expiretoken
                };
                _db.UserTokens.AddAsync(userToken);
                _db.SaveChangesAsync();
                return Ok(jwttoken);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
