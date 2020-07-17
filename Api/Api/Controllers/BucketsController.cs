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
    public class BucketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BucketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Buckets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BucketVm>>> GetHistoryItems()
        {
            var bucketEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.IsPaymentCompleted == false)
                .ToListAsync();

            IEnumerable<BucketVm> bucketVms = Mapper.Map<IEnumerable<BucketVm>>(bucketEntities);

            return Ok(bucketVms);
        }

        //GET: Buckets/userName
        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<BucketVm>>> GetHistoryUserItems(string userName)
        {
            var bucketEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.IsPaymentCompleted == false)
                .Where(o => o.ClientId == userName)
                .ToListAsync();

            IEnumerable<BucketVm> bucketVms = Mapper.Map<IEnumerable<BucketVm>>(bucketEntities);

            return Ok(bucketVms);
        }

        // GET: Buckets/userName&date
        [HttpGet("{userName}&{date}")]
        public async Task<ActionResult<BucketVm>> GetHistoryItem(string userName, DateTime date)
        {
            var bucketEntity = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.IsPaymentCompleted == false)
                .Where(o => o.ClientId == userName)
                .Where(o => o.OrderDate == date)
                .FirstOrDefaultAsync();

            if (bucketEntity == null)
            {
                return NotFound();
            }

            BucketVm bucketVm = Mapper.Map<BucketVm>(bucketEntity);

            return Ok(bucketVm);
        }

        //// PUT: Buckets/5
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

        // POST: Buckets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BucketVm>> PostHistory(BucketVm bucket)
        {
            Order order = Mapper.Map<Order>(bucket);

            order.IsPaymentCompleted = false;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = order.Id }, order);
        }

        // DELETE: Buckets/date
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
