using VueShopServer.Api.Entities;
using VueShopServer.Api.Module;

namespace VueShopServer.Api.Services
{
    public interface IUserService
    {
        User GetUserById(int id);

        User GetUserByName(string uername);

        User Add(User user);

        bool ValidatePassword(User user, string password);

        string GenerateToken(User user);
    }
}
