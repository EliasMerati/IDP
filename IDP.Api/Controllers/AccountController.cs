using IDP.Application.Context;
using IDP.Application.LogRepository;
using IDP.Common.Security;
using IDP.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IIDPContext _db;
        private readonly ILogRepository _log;
        public AccountController(IConfiguration config, IIDPContext db,ILogRepository log)
        {
            _config = config;
            _db = db;
            _log = log;
        }
        [HttpPost]
        public IActionResult Post(string UserName, string Password, int Id)
        {
            var user = _db.Users.Find(Id);
            bool existUser = _db.Users.Any(u => u.UserName == UserName && u.Password == Password && u.IsActive);

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
                    UserId = user.UserId,
                    Token = HashHelper.CreateHash(jwttoken),
                    TokenExpire = expiretoken
                };
                _db.UserTokens.AddAsync(userToken);
                _db.SaveChangesAsync();

                _log.execute(new LogInfo {Message = "Create JWT" });
                return Ok(jwttoken);
            }
            else
            {
                _log.execute(new LogInfo { Message = "Not Create JWT" });
                return NotFound();
            }
        }

    }
}
