using System;
using System.Threading.Tasks;
using Api.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IUserService _userService;

        public SignInController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserVm>> RegisterUser(UserVm userVm)
        {
            try
            {
                userVm = await _userService.AddOrUpdate(userVm);
            }
            catch (Exception e)
            {
                return StatusCode(406, new { message = e.Message, status = 406});
            }

            return StatusCode(201, new { user = userVm, status = 201 });
        }
    }
}
