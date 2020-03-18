using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VueShopServer.Api.Services;
using VueShopServer.Api.Entities;
using VueShopServer.Api.Module;

namespace VueShopServer.Api.Controllers
{
    [Authorize]
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
            var response = new ApiResult<Product>();
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                response.Success = false;
                response.Message = "Product not found.";
                return NotFound(response);
            }
            response.Success = true;
            response.Result = product;
            return Ok(response);

        }

        [HttpGet("list/{page:int}")]
        public ActionResult<ApiResult<List<Product>>> List(int page = 1)
        {
            var response = new ApiResult<List<Product>>();
            var products = _productService.GetProducts(page);
            response.Success = true;
            response.Result = products;
            return Ok(response);
        }

    }
}
