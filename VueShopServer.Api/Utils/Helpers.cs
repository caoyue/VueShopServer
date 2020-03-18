using VueShopServer.Api.Entities;
using VueShopServer.Api.Module;

namespace VueShopServer.Api.Utils
{
    public static class Helpers
    {
        public static AuthUser ToAuthUser(this User user, string token) =>
            new AuthUser
            {
                Id = user.Id,
                Username = user.Username,
                Password = "",
                Token = token
            };

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNull(this object o) => o == null;
    }
}
