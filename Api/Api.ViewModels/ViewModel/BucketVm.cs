using System.Collections.Generic;

namespace Api.ViewModels.ViewModel
{
    public class BucketVm
    {
        public int id { get; set; }
        public double price { get; set; }
        public bool status { get; set; }
        public IList<OrderVm> items { get; set; }
    }
}
