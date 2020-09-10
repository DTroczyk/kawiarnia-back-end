using Api.ViewModels.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IBucketService
    {
        public Task<IEnumerable<OrderVm>> GetBucketItems(string username);
    }
}
