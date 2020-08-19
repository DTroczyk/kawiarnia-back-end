using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService )
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login(string username, string pass)
        {
            var user = _loginService.Login(username, pass);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _loginService.GenerateJSONWebToken(user);
            UserVm userVm = Mapper.Map<UserVm>(user);

            return Ok(new { token = token, status = 200, user = userVm });
        }
    }
}
