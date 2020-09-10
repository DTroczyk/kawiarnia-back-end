using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Api.ViewModels.ViewModel;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBucketService _bucketService;

        public BucketsController(IUserService userService, IBucketService bucketService)
        {
            _bucketService = bucketService;
            _userService = userService;
        }

        // GET: Buckets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetBucketItems()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            var orderVms = await _bucketService.GetBucketItems(username);
            return Ok(orderVms);
        }
    }
}
