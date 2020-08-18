using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.BLL.Entity;
using Api.ViewModels.ViewModel;
using Api.DAL.EF;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
                return StatusCode(406, new { message = e.Message });
            }

            return StatusCode(201, userVm);
        }
    }
}
