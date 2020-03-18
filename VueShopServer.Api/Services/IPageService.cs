using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Services
{
    public interface IPageService
    {
        Page GetByTag(string tag);
    }
}
