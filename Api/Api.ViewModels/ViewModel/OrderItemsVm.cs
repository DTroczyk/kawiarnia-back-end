using System.Collections.Generic;

namespace Api.ViewModels.ViewModel
{
    public class OrderItemsVm
    {
        public IList<OrderVm> orderedProducts { get; set; }
        public AddressVm address { get; set; } 
        public string token { get; set; }
    }
}
