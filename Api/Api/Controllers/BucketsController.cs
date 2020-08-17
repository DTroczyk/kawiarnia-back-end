using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.BLL.Entity;
using Api.DAL.EF;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Api.ViewModels.ViewModel;

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
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetBucketItems()
        {
            var username = getUserName();

            var bucketEntity = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            if (bucketEntity == null || bucketEntity.Items.Count == 0)
            {
                return NoContent();
            }

            IList<OrderItem> orderItems = new List<OrderItem>();

            foreach (OrderItem order in bucketEntity.Items)
            {
                orderItems.Add(order);
            }

            IEnumerable<OrderVm> orderVms = Mapper.Map<IEnumerable<OrderVm>>(orderItems);

            return Ok(orderVms);
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
