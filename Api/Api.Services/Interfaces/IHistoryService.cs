using Api.ViewModels.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IHistoryService
    {
        public Task<IEnumerable<HistoryVm>> GetHistories(string username);
        public Task<HistoryVm> GetHistory(int id, string username);
        public Task<int> CountHistoryItems(string username);
    }
}
