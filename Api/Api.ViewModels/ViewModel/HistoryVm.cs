using System;
using System.Collections.Generic;

namespace Api.ViewModels.ViewModel
{
    public class HistoryVm
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public double price { get; set; }
        public bool status { get; set; }
        public string paymentMethod { get; set; }
        public IList<OrderVm> items { get; set; }
        public string paymentCard { get; set; }
    }
}
