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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string getUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            return claims[0].Value;
        }

        // GET: Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderVm>> GetOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            OrderVm orderVm = Mapper.Map<OrderVm>(orderItem);

            return Ok(orderVm);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderVm>> PostOrderItem(OrderVm orderVm)
        {
            var username = getUserName();

            var bucketEntity = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            if (bucketEntity == null)
            {
                bucketEntity = new Order();
                bucketEntity.IsPaymentCompleted = false;
                bucketEntity.ClientId = username;
                bucketEntity.Items = new List<OrderItem>();

                _context.Orders.Add(bucketEntity);
                await _context.SaveChangesAsync();

                bucketEntity = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);
            }

            OrderItem orderItem = Mapper.Map<OrderItem>(orderVm);
            orderItem.OrderId = bucketEntity.Id;

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }
    }
}
