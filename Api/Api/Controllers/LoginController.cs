using Api.Services.Interfaces;
using Api.ViewModels.DTOs;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            var user = _loginService.Login(login.username, login.password);

            if (user == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect.", status = 401});
            }

            var token = _loginService.GenerateJSONWebToken(user);
            UserVm userVm = Mapper.Map<UserVm>(user);

            return Ok(new { token = token, status = 200, user = userVm });
        }
    }
}
