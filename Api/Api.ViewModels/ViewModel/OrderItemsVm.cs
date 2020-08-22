using System.Collections.Generic;

namespace Api.ViewModels.ViewModel
{
    public class OrderItemsVm
    {
        public IList<OrderVm> items { get; set; }
        public AddressVm address { get; set; } 
    }
}
