using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Services
{
    public class BucketService : BaseService, IBucketService
    {
        public BucketService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OrderVm>> GetBucketItems(string username)
        {
            var bucketEntity = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.ClientId == username)
                .FirstOrDefaultAsync(o => o.IsPaymentCompleted == false);

            if (bucketEntity == null || bucketEntity.Items.Count == 0)
            {
                return new List<OrderVm>();
            }

            IList<OrderItem> orderItems = new List<OrderItem>();

            foreach (OrderItem order in bucketEntity.Items)
            {
                orderItems.Add(order);
            }

            IEnumerable<OrderVm> orderVms = Mapper.Map<IEnumerable<OrderVm>>(orderItems);

            return orderVms;
        }
    }
}
