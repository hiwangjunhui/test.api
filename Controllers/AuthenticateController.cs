using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Authentication.Models;

namespace Test.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly JwtSetting _jwtSetting;

        public AuthenticateController(JwtSetting jwtSetting)
        {
            _jwtSetting = jwtSetting;
        }

        [HttpPost]
        public object RefreshToken(User user)
        {
            if (user.Name != "Aiden" || user.Password != "123456")
            {
                return "Incorrect Name or Password.";
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_jwtSetting.Issuer,
                _jwtSetting.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(_jwtSetting.ExpireTimeSpan),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return new
            {
                token = token,
                expires = DateTime.Now.Add(_jwtSetting.ExpireTimeSpan),
                user = user
            };
        }
    }
}