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

        public async Task<OrderVm> AddOrderItem(OrderVm orderVm, string username)
        {
            var bucketEntity = await _dbContext.Orders
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
                bucketEntity.PostalCode = String.Empty;
                bucketEntity.Street = String.Empty;
                bucketEntity.City = String.Empty;
                bucketEntity.HouseNumber = String.Empty;

                _dbContext.Orders.Add(bucketEntity);
                await _dbContext.SaveChangesAsync();

                bucketEntity = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);
            }

            OrderItem orderItem = Mapper.Map<OrderItem>(orderVm);
            orderItem.OrderId = bucketEntity.Id;
            orderItem.PaymentStatus = (PaymentStatus)1;

            _dbContext.OrderItems.Add(orderItem);
            await _dbContext.SaveChangesAsync();

            return orderVm;
        }

        public async Task<OrderVm> DeleteOrderItem(int id, string username)
        {
            var orderItem = await _dbContext.OrderItems
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == id);
            if (orderItem == null)
            {
                throw new Exception("Order item is null");
            }
            if (orderItem.Order.ClientId.ToUpper() != username.ToUpper())
            {
                throw new Exception("Method Not Allowed");
            }

            _dbContext.OrderItems.Remove(orderItem);
            await _dbContext.SaveChangesAsync();

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
