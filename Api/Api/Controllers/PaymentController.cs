using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.BLL.Entity;
using Api.BLL.ViewModel;
using Api.DAL.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string getUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            return claims[0].Value;
        }

        [HttpPost]
        public async Task<ActionResult<Session>> StartPayment(IList<OrderVm> orderVms)
        {
            if (orderVms.Count == 0 || orderVms == null)
            {
                return StatusCode(406, new { error = "The order is null or empty." });
            }

            var username = getUserName();

            var bucketEntity = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            if (bucketEntity == null || bucketEntity.Items.Count == 0)
            {
                return StatusCode(406, new { error = "The buckets is empty." });
            }
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            List<SessionLineItemOptions> lineItem = new List<SessionLineItemOptions>();
            foreach (OrderVm order in orderVms)
            {
                var item = bucketEntity.Items.FirstOrDefault(i => i.Id == order.id);
                lineItem.Add(new SessionLineItemOptions
                {
                    Quantity = 1,
                    Amount = (int)(item.Price * 100),
                    Currency = "pln",
                    Name = item.CoffeeId,
                    Description = item.Coffee.Description
                });
                item.PaymentStatus = (PaymentStatus)2;
                _context.OrderItems.Update(item);
                await _context.SaveChangesAsync();
            }


            StripeConfiguration.ApiKey = "xxx";
            var options = new SessionCreateOptions
            {
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                    "p24"
                },
                LineItems = lineItem,
                Mode = "payment",
                CustomerEmail = user.Email,
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(session.ToJson());
        }
    }
}
