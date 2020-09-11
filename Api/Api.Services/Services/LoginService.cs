using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Api.Services.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private IConfiguration _config;

        public LoginService(ApplicationDbContext dbContext, IConfiguration config) : base(dbContext)
        {
            _config = config;
        }

        public User AuthenticateUser(User login)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.UserName == login.UserName);
            if (user == null)
            {
                return null;
            }
            if (login.UserName.ToUpper() == user.UserName.ToUpper() && (PasswordHashService.ValidatePassword(login.PasswordHash, user) || login.PasswordHash == user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        public string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        public User Login(string username, string pass)
        {
            User login = new User();
            login.UserName = username;
            login.PasswordHash = pass;

            var user = AuthenticateUser(login);

            if (user != null)
            {
                return user;
            }
            
            return null;
        }

    }
}
