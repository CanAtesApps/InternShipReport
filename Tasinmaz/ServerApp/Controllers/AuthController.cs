using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Data;
using ServerApp.Models;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly TasinmazContext _context;
        private readonly IConfiguration _config;
        public AuthController(TasinmazContext context, IConfiguration config)
        {
            _context = context;
            _config  = config;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate (LoginWiewModel model)
        {
            var tmp = findAndReturn(model);
            if(tmp == null || !tmp.isActive)
            {
                Logger.log(_context,(model.username)+ "  - Hatalı giriş",false);
                await _context.SaveChangesAsync();

                return BadRequest(new { message = "kullanıcı adı veya parola hatalı"});
            }

            Logger.log(_context,(model.username)+ "  -  Basarili giriş",true);
            await _context.SaveChangesAsync();
            
            return Ok(new {
                result = tmp,
                token = JwtTokenGeneratorMachine(tmp)
            });
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout (LoginWiewModel model)
        {
           Logger.log(_context,(model.username + " kullanici cikisi"),true);
           await _context.SaveChangesAsync();
           return Ok();
        }
        private Kullanici findAndReturn(LoginWiewModel model)
        {
            return _context.Kullanici.FirstOrDefault(x =>  x.Email    == model.username
                                                        && x.Password == model.password);
        }
        private string JwtTokenGeneratorMachine(Kullanici userInfo)  
        {  
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.UserID.ToString()),
                new Claim(ClaimTypes.Name, userInfo.Email)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(_config.GetSection("AppSettings:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }   
    }
}
   