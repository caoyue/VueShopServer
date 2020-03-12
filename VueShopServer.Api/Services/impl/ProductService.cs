using System.Collections.Generic;
using System.Linq;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Apple",
                Price = 3.11M,
                Stock = 101,
                Detail = "This is an apple"
            },
            new Product
            {
                Id = 2,
                Name = "Banana",
                Price = 2.37M,
                Stock = 39,
                Detail = "This is a banana"
            },
        };

        public ProductService()
        {
        }

        public List<Product> GetProducts(int page = 1) => products;

        public Product GetProductById(int id) => products.FirstOrDefault(p => p.Id == id);

    }
}
