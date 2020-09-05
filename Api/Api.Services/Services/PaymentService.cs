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
            DotNetEnv.Env.Load();
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
            bucketEntity.Latitude = 0;
            bucketEntity.Longitude = 0;
            bucketEntity.PaymentCard = String.Empty;
            _dbContext.Update(bucketEntity);

            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task<bool> Payment(OrderItemsVm itemsVm, string username)
        {
            var orderVms = itemsVm.orderedProducts;
            var addressVm = itemsVm.address;
            var token = itemsVm.token;

            StripeConfiguration.ApiKey = System.Environment.GetEnvironmentVariable("STRIPE_SKKEY");

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

            long dbPriceSum = 0;

            foreach (OrderVm order in orderVms)
            {
                var item = bucketEntity.Items.FirstOrDefault(i => i.Id == order.id);
                dbPriceSum += (long)(item.Price * 100);

                if (item.Id != order.id)
                {
                    throw new Exception("The order's id is invalid");
                }

                item.PaymentStatus = (PaymentStatus)2;
                _dbContext.OrderItems.Update(item);
            }
            await _dbContext.SaveChangesAsync();

            var amount = (long)(orderVms.Sum(o => o.price) * 100);
            if (amount != dbPriceSum)
            {
                return false;
            }

            bucketEntity.City = addressVm.place;
            bucketEntity.Street = addressVm.road;
            bucketEntity.HouseNumber = addressVm.houseNumber;
            bucketEntity.PostalCode = addressVm.zipcode;
            bucketEntity.Latitude = addressVm.latLng.latitude;
            bucketEntity.Longitude = addressVm.latLng.longitude;
            _dbContext.Update(bucketEntity);
            await _dbContext.SaveChangesAsync();

            var options = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = "pln",
                Description = "Płatność",
                Source = token
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            bucketEntity.PaymentMethod = charge.PaymentMethodDetails.Card.Brand;
            bucketEntity.PaymentCard = "**** **** **** " + charge.PaymentMethodDetails.Card.Last4;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PaymentOnDelivery(OrderItemsVm itemsVm, string username)
        {
            var orderVms = itemsVm.orderedProducts;
            var addressVm = itemsVm.address;

            if (orderVms.Count == 0 || orderVms == null)
            {
                throw new Exception("The order is null or empty");
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

            long dbPriceSum = 0;

            foreach (OrderVm order in orderVms)
            {
                var item = bucketEntity.Items.FirstOrDefault(i => i.Id == order.id);
                dbPriceSum += (long)(item.Price * 100);

                if (item.Id != order.id)
                {
                    throw new Exception("The order's id is invalid");
                }

                item.PaymentStatus = (PaymentStatus)2;
                _dbContext.OrderItems.Update(item);
            }
            await _dbContext.SaveChangesAsync();

            var amount = (long)(orderVms.Sum(o => o.price) * 100);
            if (amount != dbPriceSum)
            {
                return false;
            }

            bucketEntity.City = addressVm.place;
            bucketEntity.Street = addressVm.road;
            bucketEntity.HouseNumber = addressVm.houseNumber;
            bucketEntity.PostalCode = addressVm.zipcode;
            bucketEntity.Latitude = addressVm.latLng.latitude;
            bucketEntity.Longitude = addressVm.latLng.longitude;
            bucketEntity.PaymentMethod = "On Delivery";
            bucketEntity.PaymentCard = "";
            _dbContext.Update(bucketEntity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task Success(string username)
        {
            var bucketEntity = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            var items = bucketEntity.Items.Where(i => i.PaymentStatus == (PaymentStatus)2);
            if (items.Count() == 0 || items == null)
            {
                throw new Exception("Bucket is empty");
            }

            foreach (BLL.Entity.OrderItem item in items)
            {
                item.PaymentStatus = (PaymentStatus)3;
                _dbContext.OrderItems.Update(item);
            }

            if (items.Count() == bucketEntity.Items.Count())
            {
                bucketEntity.IsPaymentCompleted = true;
                bucketEntity.OrderDate = DateTime.UtcNow.AddHours(2);
                _dbContext.Orders.Update(bucketEntity);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var newBucket = new BLL.Entity.Order();
                newBucket.ClientId = bucketEntity.ClientId;
                _dbContext.Orders.Add(newBucket);
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
                bucketEntity.OrderDate = DateTime.UtcNow.AddHours(2);
                _dbContext.Orders.Update(bucketEntity);
                await _dbContext.SaveChangesAsync();
            }

            return;
        }
    }
}
