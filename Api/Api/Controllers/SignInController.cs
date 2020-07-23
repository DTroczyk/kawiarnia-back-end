using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.BLL.Entity;
using Api.BLL.ViewModel;
using Api.DAL.EF;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SignInController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserVm>> GetUser(string username)
        {
            var userEntity = await _context.Users.FindAsync(username);

            if (userEntity == null)
            {
                return NotFound();
            }

            UserVm userVm = Mapper.Map<UserVm>(userEntity);

            return Ok(userVm);
        }

        [HttpPost]
        public async Task<ActionResult<UserVm>> RegisterUser(UserVm userVm)
        {
            var user = Mapper.Map<User>(userVm);
            user.RegistrationDate = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { username = user.UserName }, user);
        }
    }
}
