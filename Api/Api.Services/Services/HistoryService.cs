using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Services
{
    public class HistoryService : BaseService, IHistoryService
    {
        public HistoryService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CountHistoryItems(string username)
        {
            var historyEntities = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.IsPaymentCompleted == true)
                .Where(o => o.ClientId == username)
                .ToListAsync();

            int counter = 0;
            foreach (Order order in historyEntities)
            {
                counter += order.Items.Count();
            }

            return counter;
        }

        public async Task<HistoryVm> GetHistoryItem(int id, string username)
        {
            var historyEntities = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.IsPaymentCompleted == true)
                .Where(o => o.ClientId == username)
                .FirstAsync(o => o.Id == id);
                

            HistoryVm historyVm = Mapper.Map<HistoryVm>(historyEntities);

            return historyVm;
        }

        public async Task<IEnumerable<HistoryVm>> GetHistoryItems(string username)
        {
            var historyEntities = await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(c => c.Coffee)
                .Where(o => o.IsPaymentCompleted == true)
                .Where(o => o.ClientId == username)
                .ToListAsync();

            IEnumerable<HistoryVm> historyVms = Mapper.Map<IEnumerable<HistoryVm>>(historyEntities);

            return historyVms;
        }
    }
}
