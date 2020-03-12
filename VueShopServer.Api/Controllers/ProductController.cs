using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VueShopServer.Api.Services;
using VueShopServer.Api.Entities;

namespace VueShopServer.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("/product/list/{page:int}")]
        public List<Product> List(int page = 1)
        {
            return _productService.GetProducts(page);
        }

        [HttpGet("product/{id:int}")]
        public Product GetById(int id)
        {
            return _productService.GetProductById(id);
        }
    }
}
