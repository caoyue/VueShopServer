using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VueShopServer.Api.Data;
using VueShopServer.Api.Entities;
using VueShopServer.Api.Utils;

namespace VueShopServer.Api.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly AppSetting _appSetting;

        public UserService(IRepository<User> userRepository, IOptions<AppSetting> appSetting)
        {
            _userRepository = userRepository;
            _appSetting = appSetting.Value;
        }

        public User GetUserByName(string username) =>
            _userRepository.AsQueryable
            .FirstOrDefault(u => u.Username == username);

        public bool ValidatePassword(User user)
        {
            var u = GetUserByName(user.Username);
            return u != null && PasswordValid(u.Password, user.Password);
        }

        public User Add(User user)
        {
            user.Password = PasswordHash(user.Password);
            return _userRepository.Insert(user);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_appSetting.ExpiredDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string PasswordHash(string password) =>
            $"mock{password}";

        private bool PasswordValid(string hashed, string password) =>
            hashed == PasswordHash(password);
    }
}
