using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Module
{
    public class AuthUser : User
    {
        public string Token { get; set; }
    }
}
