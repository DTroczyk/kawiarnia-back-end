using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.BLL.Entity;
using Api.DAL.EF;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Authorize]
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
        [HttpGet]
        public async Task<ActionResult<UserVm>> GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userVm = await _userService.GetCurrentUser(identity);

            return Ok(userVm);
        }


        // PUT: Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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

            return Ok(userVm);
        }

        //// DELETE: Users/userName
        //[HttpDelete("{userName}")]
        //public async Task<ActionResult<User>> DeleteUser(string userName)
        //{
        //    if (!Autorization(userName))
        //    {
        //        return Unauthorized();
        //    }

        //    var user = await _context.Users.FindAsync(userName);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return user;
        //}
    }
}
