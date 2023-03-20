using IMAOCMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMAOCMS.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost,Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
                return BadRequest("Invalid Client request");
            if (user.UserName=="Mehmet"&&user.Password=="Elçi")
            {
                var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials=new SigningCredentials(secretkey,SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken
                    (
                    issuer: "https://localhost:7091",
                    audience: "https://localhost:7091",
                    claims:new List<Claim>(),
                    expires:DateTime.Now.AddMinutes(5),
                    signingCredentials:signingCredentials
                    );
                var tokenString=new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token= tokenString });
            }
            return Unauthorized();
        }
    }
}
