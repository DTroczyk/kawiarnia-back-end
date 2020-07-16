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

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Histories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryVm>>> GetHistoryItems()
        {
            var historyEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.PaymentCompleted == true)
                .ToListAsync();

            IEnumerable<HistoryVm> historyVms = Mapper.Map<IEnumerable<HistoryVm>>(historyEntities);

            return Ok(historyVms);
        }

        //GET: Histories/userName
        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<HistoryVm>>> GetHistoryUserItems(string userName)
        {
            var historyEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.PaymentCompleted == true)
                .Where(o => o.ClientId == userName)
                .ToListAsync();

            IEnumerable<HistoryVm> historyVms = Mapper.Map<IEnumerable<HistoryVm>>(historyEntities);

            return Ok(historyVms);
        }

        // GET: Histories/userName&date
        [HttpGet("{userName}&{date}")]
        public async Task<ActionResult<HistoryVm>> GetHistoryItem(string userName, DateTime date)
        {
            var historyEntity = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.PaymentCompleted == true)
                .Where(o => o.ClientId == userName)
                .Where(o => o.OrderDate == date)
                .FirstOrDefaultAsync();

            if (historyEntity == null)
            {
                return NotFound();
            }

            HistoryVm historyVm = Mapper.Map<HistoryVm>(historyEntity);

            return Ok(historyVm);
        }

        //// PUT: Histories/5
        //[HttpPut("{date}")]
        //public async Task<IActionResult> PutOrderItem(DateTime date, HistoryVm historyVm)
        //{
        //    if (date != historyVm.date)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(historyVm).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HistoryExists(date))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: Histories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HistoryVm>> PostHistory(HistoryVm history)
        {
            Order order = Mapper.Map<Order>(history);

            order.PaymentCompleted = false;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = order.Id }, order);
        }

        // DELETE: histories/date
        [HttpDelete("{date}")]
        public async Task<ActionResult<Order>> DeleteHistory(DateTime date)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderDate == date);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool HistoryExists(DateTime id)
        {
            return _context.Orders.Any(e => e.OrderDate == id);
        }
    }
}
