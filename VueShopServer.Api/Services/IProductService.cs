using System.Collections.Generic;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Services
{
    public interface IProductService
    {
        List<Product> GetProducts(int page = 1);

        Product GetProductById(int id);
    }
}
