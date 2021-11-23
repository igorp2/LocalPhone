using LocalPhoneDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace LocalPhoneApi.Controllers
{
    [ApiController]
    [EnableCors("Policy")]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly List<User> appUsers;

        public LoginController()
        {
            appUsers = new List<User> { new User { UserName = "useradmin", Password = "123456" } };
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();

            User user = appUsers.SingleOrDefault(x => x.UserName == login.UserName);

            if (user != null && user.UserName != null)
            {
                var tokenString = GenerateJWTToken(user);
 
                response = Ok(new
                {
                    token = tokenString,
                });
            }
            return response;
        }

        [HttpGet("ValidateToken")]
        [AllowAnonymous]
        public bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("af0e52040f694981a3e212b5b795d52d2d86fd10399742cbbb7882600abf974082600abf9740")); 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var name = string.Empty;
            var email = string.Empty;
            string role = "admin"; 

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("NAME", name),
                new Claim("USERNAME", userInfo.UserName),
                new Claim("EMAIL", email),
                new Claim("role", role),
            };

            var token = new JwtSecurityToken(
            issuer: "https://localhost:44334/", 
            audience: "https://localhost:44334/", 
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:44334/",
                ValidAudience = "https://localhost:44334/",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("af0e52040f694981a3e212b5b795d52d2d86fd10399742cbbb7882600abf974082600abf9740")),
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
