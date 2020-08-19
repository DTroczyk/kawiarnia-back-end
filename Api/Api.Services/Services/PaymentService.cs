using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Services
{
    public class PaymentService : BaseService, IPaymentService
    {
        public PaymentService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ActionResult<Session>> StatrPayment(IList<OrderVm> orderVms, string username)
        {
            if (orderVms.Count == 0 || orderVms == null)
            {
                throw new Exception("The order is null or empty.");
            }

            var bucketEntity = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            if (bucketEntity == null || bucketEntity.Items.Count == 0)
            {
                throw new Exception("Bucket is empty");
            }
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

            List<SessionLineItemOptions> lineItem = new List<SessionLineItemOptions>();
            foreach (OrderVm order in orderVms)
            {
                var item = bucketEntity.Items.FirstOrDefault(i => i.Id == order.id);

                if (item.Id != order.id)
                {
                    throw new Exception("The order's id is invalid.");
                }

                lineItem.Add(new SessionLineItemOptions
                {
                    Quantity = 1,
                    Amount = (long)(item.Price * 100),
                    Currency = "pln",
                    Name = item.CoffeeId,
                    Description = item.Coffee.Description
                });
                item.PaymentStatus = (PaymentStatus)2;
                _dbContext.OrderItems.Update(item);
            }
            await _dbContext.SaveChangesAsync();

            StripeConfiguration.ApiKey = "xxx";
            var FrontURL = new
            {
                successUrl = "https://quirky-bose-7d6097.netlify.app/panel/success",
                cancelUrl = "https://quirky-bose-7d6097.netlify.app/panel/cancel",
            };
            var options = new SessionCreateOptions
            {
                SuccessUrl = FrontURL.successUrl,
                CancelUrl = FrontURL.cancelUrl,
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

            return session;
        }
    }
}
