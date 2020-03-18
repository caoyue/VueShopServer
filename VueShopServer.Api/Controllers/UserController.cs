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
            var result = new ApiResult<AuthUser>();
            var u = _userService.GetUserByName(user.Username);
            if (u.IsNull())
            {
                u = _userService.Add(user);
                var token = _userService.GenerateToken(u);
                result.Success = true;
                result.Result = u.ToAuthUser(token);
                return Ok(result);
            }
            result.Success = false;
            result.Message = "Username has been taken.";
            return Ok(result);
        }

        [HttpPost("login")]
        public ActionResult<ApiResult<AuthUser>> Login(User user)
        {
            var result = new ApiResult<AuthUser>();
            if (!_userService.ValidatePassword(user))
            {
                result.Success = false;
                result.Message = "Login failed.";
                return Ok(result);
            }

            result.Success = true;
            result.Result = new AuthUser
            {
                Username = user.Username,
                Token = _userService.GenerateToken(user)
            };
            return Ok(result);
        }
    }
}
