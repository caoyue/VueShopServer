using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VueShopServer.Api.Services;
using VueShopServer.Api.Entities;
using VueShopServer.Api.Module;

namespace VueShopServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) =>
            _productService = productService;


        [HttpGet("{id:int}")]
        public ActionResult<ApiResult<Product>> GetById(int id)
        {
            var result = new ApiResult<Product>();
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                result.Success = false;
                result.Message = "Product not found.";
                return NotFound(result);
            }
            result.Success = true;
            result.Result = product;
            return Ok(result);

        }

        [HttpGet("list/{page:int}")]
        public ActionResult<ApiResult<List<Product>>> List(int page = 1)
        {
            var result = new ApiResult<List<Product>>();
            var products = _productService.GetProducts(page);
            result.Success = true;
            result.Result = products;
            return Ok(result);
        }

    }
}
