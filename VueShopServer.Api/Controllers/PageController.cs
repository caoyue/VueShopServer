using Microsoft.AspNetCore.Mvc;
using VueShopServer.Api.Entities;
using VueShopServer.Api.Module;
using VueShopServer.Api.Services;
using VueShopServer.Api.Utils;

namespace VueShopServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService) =>
            _pageService = pageService;

        [HttpGet("tag/{tag}")]
        public ActionResult<ApiResult<Page>> Tag(string tag)
        {
            var response = new ApiResult<Page>();
            var page = _pageService.GetByTag(tag);
            if (page.IsNull())
            {
                response.Success = false;
                response.Message = "Not found.";
                return NotFound(response);
            }

            response.Success = true;
            response.Result = page;
            return Ok(response);
        }
    }
}
