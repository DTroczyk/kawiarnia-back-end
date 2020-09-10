using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public OrderVm AddOrderItem(OrderVm orderVm, string username)
        {
            if (orderVm == null)
            {
                throw new Exception("Order item is null");
            }

            var bucketEntity = _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefault(o => o.IsPaymentCompleted == false);

            if (bucketEntity == null)
            {
                bucketEntity = new Order();
                bucketEntity.IsPaymentCompleted = false;
                bucketEntity.ClientId = username;
                bucketEntity.Items = new List<OrderItem>();
                bucketEntity.PostalCode = String.Empty;
                bucketEntity.Street = String.Empty;
                bucketEntity.City = String.Empty;
                bucketEntity.HouseNumber = String.Empty;
                bucketEntity.PaymentCard = String.Empty;

                _dbContext.Orders.Add(bucketEntity);
                _dbContext.SaveChanges();

                bucketEntity = _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefault(o => o.IsPaymentCompleted == false);
            }

            OrderItem orderItem = Mapper.Map<OrderItem>(orderVm);
            orderItem.OrderId = bucketEntity.Id;
            orderItem.PaymentStatus = (PaymentStatus)1;

            // Set price
            double price = 0;
            price += orderItem.MilkCount * 1;
            price += orderItem.EspressoCount * 1.5;
            orderItem.Price = price;

            _dbContext.OrderItems.Add(orderItem);
            _dbContext.SaveChanges();

            return orderVm;
        }

        public OrderVm DeleteOrderItem(int id, string username)
        {
            var orderItem = _dbContext.OrderItems
                .Include(oi => oi.Order)
                .FirstOrDefault(oi => oi.Id == id);
            if (orderItem == null)
            {
                throw new Exception("Order item is null");
            }
            if (orderItem.Order.ClientId.ToUpper() != username.ToUpper())
            {
                throw new Exception("Method Not Allowed");
            }

            _dbContext.OrderItems.Remove(orderItem);
            _dbContext.SaveChanges();

            var orderVm = Mapper.Map<OrderVm>(orderItem);
            return orderVm;
        }

        public async Task<OrderVm> GetOrderItem(int orderId)
        {
            var orderEntity = await _dbContext.OrderItems.FirstOrDefaultAsync(oi => oi.Id == orderId);
            var orderVm = Mapper.Map<OrderVm>(orderEntity);
            return orderVm;
        }
    }
}
