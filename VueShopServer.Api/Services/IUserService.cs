using VueShopServer.Api.Entities;
using VueShopServer.Api.Module;

namespace VueShopServer.Api.Services
{
    public interface IUserService
    {
        User GetUserByName(string uername);

        User Add(User user);

        bool ValidatePassword(User user);

        string GenerateToken(User user);
    }
}
