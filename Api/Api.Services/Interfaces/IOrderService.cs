using Api.ViewModels.ViewModel;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderVm> GetOrderItem(int orderId);
        public OrderVm AddOrderItem(OrderVm orderVm, string username);
        public OrderVm DeleteOrderItem(int id, string username);
    }
}
