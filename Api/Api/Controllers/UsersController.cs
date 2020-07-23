using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.BLL.Entity;
using Api.DAL.EF;
using Api.BLL.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool Autorization()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();

            if (claims[0].Value.ToUpper() == "DTROCZYK")
            {
                return true;
            }

            return false;
        }

        private bool Autorization(string username)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();

            if (claims[0].Value.ToUpper() == username.ToUpper())
            {
                return true;
            }

            return false;
        }

        // GET: Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVm>>> GetUsers()
        {
            if (Autorization())
            {
                var userEntities = await _context.Users.ToListAsync();
                IEnumerable<UserVm> userVms = Mapper.Map<IEnumerable<UserVm>>(userEntities);

                return Ok(userVms);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();

            var userEntity = await _context.Users.Where(x => x.UserName == claims[0].Value).ToListAsync();
            IEnumerable<UserVm> userVm = Mapper.Map<IEnumerable<UserVm>>(userEntity);

            return Ok(userVm);
        }

        //// GET: Users/userName
        //[Authorize]
        //[HttpGet("{userName}")]
        //public async Task<ActionResult<UserVm>> GetUser(string userName)
        //{
        //    if (!Autorization())
        //    {
        //        return Unauthorized();
        //    }

        //    var user = await _context.Users.FindAsync(userName);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    UserVm userVm = Mapper.Map<UserVm>(user);

        //    return Ok(userVm);
        //}

        

        // PUT: Users/userName
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{userName}")]
        public async Task<IActionResult> PutUser(string userName, UserVm userVm)
        {
            if (!Autorization(userName))
            {
                return Unauthorized();
            }

            var user = Mapper.Map<User>(userVm);

            if (userName != user.UserName)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        

        // DELETE: Users/5
        [Authorize]
        [HttpDelete("{userName}")]
        public async Task<ActionResult<User>> DeleteUser(string userName)
        {
            if (!Autorization(userName))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(string userName)
        {
            return _context.Users.Any(e => e.UserName == userName);
        }
    }
}
