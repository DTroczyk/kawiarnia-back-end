using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
