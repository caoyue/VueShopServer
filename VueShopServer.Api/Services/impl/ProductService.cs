using System.Collections.Generic;
using System.Linq;
using VueShopServer.Api.Data;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
            Init();
        }

        private void Init()
        {
            if (!_productRepository.AsQueryable.Any())
            {
                _productRepository.Insert(new Product
                {
                    Id = 1,
                    Name = "Apple",
                    Price = 3.11M,
                    Stock = 101,
                    Detail = "This is an apple"
                });
                _productRepository.Insert(new Product
                {
                    Id = 2,
                    Name = "Banana",
                    Price = 2.37M,
                    Stock = 39,
                    Detail = "This is a banana"
                });
            }
        }

        public List<Product> GetProducts(int page = 1)
            => _productRepository.AsQueryable.ToList();


        public Product GetProductById(int id)
            => _productRepository.AsQueryable.FirstOrDefault(p => p.Id == id);
    }
}
