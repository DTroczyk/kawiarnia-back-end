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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string getUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            return claims[0].Value;
        }

        // GET: Histories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryVm>>> GetHistoryItems()
        {
            var username = getUserName();

            var historyEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.IsPaymentCompleted == true)
                .Where(o => o.ClientId == username)
                .ToListAsync();

            int counter = 0;
            foreach (Order order in historyEntities)
            {
                counter += order.Items.Count();
            }

            IEnumerable<HistoryVm> historyVms = Mapper.Map<IEnumerable<HistoryVm>>(historyEntities);

            return Ok(new { counter = counter, historyVms });
        }

        //GET: Histories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HistoryVm>>> GetHistoryUserItems(int id)
        {
            var username = getUserName();

            var historyEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.IsPaymentCompleted == true)
                .Where(o => o.ClientId == username)
                .Where(o => o.Id == id)
                .ToListAsync();

            IEnumerable<HistoryVm> historyVms = Mapper.Map<IEnumerable<HistoryVm>>(historyEntities);

            return Ok(historyVms);
        }

        private bool HistoryExists(DateTime id)
        {
            return _context.Orders.Any(e => e.OrderDate == id);
        }
    }
}
