using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VueShopServer.Api.Entities;
using VueShopServer.Api.Module;
using VueShopServer.Api.Services;
using VueShopServer.Api.Utils;

namespace VueShopServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) =>
            _userService = userService;

        [HttpPost("register")]
        public ActionResult<ApiResult<AuthUser>> Register(User user)
        {
            var response = new ApiResult<AuthUser>();
            var u = _userService.GetUserByName(user.Username);
            if (u.IsNull())
            {
                u = _userService.Add(user);
                var token = _userService.GenerateToken(u);
                response.Success = true;
                response.Result = u.ToAuthUser(token);
                return Ok(response);
            }
            response.Success = false;
            response.Message = "Username has been taken.";
            return Ok(response);
        }

        [HttpPost("login")]
        public ActionResult<ApiResult<AuthUser>> Login(User user)
        {
            var response = new ApiResult<AuthUser>();
            var u = _userService.GetUserByName(user.Username);

            if (u.IsNull() || !_userService.ValidatePassword(u, user.Password))
            {
                response.Success = false;
                response.Message = "Login failed.";
                return Ok(response);
            }

            response.Success = true;
            response.Result = u.ToAuthUser(_userService.GenerateToken(u));
            return Ok(response);
        }

        [Authorize]
        [HttpGet("profile")]
        public ActionResult<ApiResult<AuthUser>> Profile()
        {
            var response = new ApiResult<AuthUser>();
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (!claim.IsNull() && int.TryParse(claim.Value, out var id))
            {
                var u = _userService.GetUserById(id);
                if (!u.IsNull())
                {
                    response.Success = true;
                    response.Result = u.ToAuthUser("");
                    return Ok(response);
                }
            }
            response.Success = false;
            response.Message = "Invalid request, please login first.";
            return Unauthorized(response);
        }
    }
}
