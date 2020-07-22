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
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BucketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string getUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            return claims[0].Value;
        }

        // GET: Buckets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BucketVm>>> GetHistoryItems()
        {
            var username = getUserName();

            var bucketEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.IsPaymentCompleted == false)
                .Where(o => o.ClientId == username)
                .ToListAsync();

            IEnumerable<BucketVm> bucketVms = Mapper.Map<IEnumerable<BucketVm>>(bucketEntities);

            return Ok(bucketVms);
        }

        //GET: Buckets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BucketVm>>> GetHistoryUserItems(int id)
        {
            var bucketEntities = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffe)
                .Where(o => o.IsPaymentCompleted == false)
                .Where(o => o.Id == id)
                .ToListAsync();

            IEnumerable<BucketVm> bucketVms = Mapper.Map<IEnumerable<BucketVm>>(bucketEntities);

            return Ok(bucketVms);
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

        // DELETE: Buckets/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteHistory(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
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
