using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
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

        public async Task Cancel(string username)
        {
            var bucketEntity = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            foreach (BLL.Entity.OrderItem item in bucketEntity.Items)
            {
                item.PaymentStatus = (PaymentStatus)1;
                _dbContext.Update(item);
            }

            bucketEntity.City = String.Empty;
            bucketEntity.Street = String.Empty;
            bucketEntity.HouseNumber = String.Empty;
            bucketEntity.PostalCode = String.Empty;
            bucketEntity.OrderDate = DateTime.MinValue;
            _dbContext.Update(bucketEntity);

            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task<Session> StatrPayment(OrderItemsVm itemsVm, string username)
        {
            var orderVms = itemsVm.items;
            var addressVm = itemsVm.address;

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

            bucketEntity.City = addressVm.place;
            bucketEntity.Street = addressVm.road;
            bucketEntity.HouseNumber = addressVm.houseNumber;
            bucketEntity.PostalCode = addressVm.zipcode;
            _dbContext.Update(bucketEntity);

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
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }

        public async Task Success(string username)
        {
            var bucketEntity = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            var items = bucketEntity.Items.Where(i => i.PaymentStatus == (PaymentStatus)2);

            foreach (BLL.Entity.OrderItem item in items)
            {
                item.PaymentStatus = (PaymentStatus)3;
                _dbContext.OrderItems.Update(item);
            }

            if (items.Count() == bucketEntity.Items.Count())
            {
                bucketEntity.IsPaymentCompleted = true;
                bucketEntity.OrderDate = DateTime.Now;
                _dbContext.Orders.Update(bucketEntity);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var newBucket = new BLL.Entity.Order();
                newBucket.ClientId = bucketEntity.ClientId;
                await _dbContext.SaveChangesAsync();

                newBucket = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .Where(o => o.Id != bucketEntity.Id)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

                var oldItems = bucketEntity.Items.Where(i => i.PaymentStatus == (PaymentStatus)1);
                foreach (BLL.Entity.OrderItem item in oldItems)
                {
                    item.OrderId = newBucket.Id;
                    _dbContext.OrderItems.Update(item);
                }
                await _dbContext.SaveChangesAsync();

                bucketEntity.IsPaymentCompleted = true;
                bucketEntity.OrderDate = DateTime.Now;
                _dbContext.Orders.Update(bucketEntity);
                await _dbContext.SaveChangesAsync();
            }

            return;
        }
    }
}
