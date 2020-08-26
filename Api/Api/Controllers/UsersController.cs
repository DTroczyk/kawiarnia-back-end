using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.ViewModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserVm>> GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userVm = await _userService.GetCurrentUser(identity);

            return Ok(userVm);
        }


        // PUT: Users
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutUser(UserVm userVm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                userVm = await _userService.AddOrUpdate(userVm, identity);
            }
            catch (Exception e)
            {
                return StatusCode(406, new { message = e.Message });
            }
            
            return Ok(new { status = 200, user = userVm });
        }

        // DELETE: Users
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<UserVm>> DeleteUser(UserVm userVm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            if (username != userVm.username)
            {
                return StatusCode(405, new { message = "Method Not Allowed. Wrong user." });
            }

            try
            {
                var user = await _userService.Delete(userVm);
                return Ok(new { status = 200, user = user });
            }
            catch (Exception e)
            {
                return StatusCode(406, new { message = e.Message });
            }
        }

        [Route("forgotten")]
        [HttpPost]
        public ActionResult<UserVm> ForgetPassword()
        {
            return Ok(new { message = "Not implemented." });
        }

    }
}
