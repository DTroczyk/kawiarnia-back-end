using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.ViewModels.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly IUserService _userService;

        public HistoriesController(IHistoryService historyService, IUserService userService)
        {
            _historyService = historyService;
            _userService = userService;
        }

        // GET: Histories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryVm>>> GetHistories()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            int counter = await _historyService.CountHistoryItems(username);
            var historyVms = await _historyService.GetHistories(username);

            return Ok(new { status = 200, counter = counter, historyVms });
        }

        //GET: Histories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryVm>> GetHistory(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = _userService.GetUserName(identity);

            var historyVm = await _historyService.GetHistory(id, username);

            return Ok(new { status = 200, historyVm });
        }
    }
}
