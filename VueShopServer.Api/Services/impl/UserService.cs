using System.Linq;
using VueShopServer.Api.Data;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository) =>
            _userRepository = userRepository;

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

        public string GenerateToken(User user) =>
            $"Bearer{user.Username}";

        private string PasswordHash(string password) =>
            $"mock{password}";

        private bool PasswordValid(string hashed, string password) =>
            hashed == PasswordHash(password);
    }
}
