using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.ViewModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Api.Services.Interfaces;
using Api.ViewModels.DTOs;

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

            return Ok(new { user = userVm, status = 200 });
        }


        // PUT: Users
        [Authorize]
        [HttpPut]
        public IActionResult PutUser(UserVm userVm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                userVm = _userService.AddOrUpdate(userVm, identity);
            }
            catch (Exception e)
            {
                return StatusCode(406, new { message = e.Message, status = 406 });
            }
            
            return Ok(new { status = 200, user = userVm });
        }

        // DELETE: Users
        [Authorize]
        [HttpDelete]
        public ActionResult<UserVm> DeleteUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            try
            {
                var user = _userService.Delete(username);
                return Ok(new { status = 200, user = user , message = "Account deleted."});
            }
            catch (Exception e)
            {
                return StatusCode(406, new { message = e.Message, status = 406 });
            }
        }

        [Route("forgotten")]
        [HttpPost]
        public async Task<ActionResult> ForgetPassword(EmailDto email)
        {
            try
            {
                if (await _userService.ForgottenPassword(email.email) == true)
                { 
                    return Ok(new { message = "Email was sent", status = 200 }); 
                }
                return StatusCode(406, new { message = "Something gone wrong", status = 406 });

            }
            catch (Exception e)
            {
                return StatusCode(406, new { message = e.Message, status = 406 });
            }
        }

    }
}
