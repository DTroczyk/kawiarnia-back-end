using Api.ViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IBucketService
    {
        public Task<IEnumerable<OrderVm>> GetBucketItems(string username);
    }
}
