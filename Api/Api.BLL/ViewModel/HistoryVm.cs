using Api.BLL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.BLL.ViewModel
{
    public class HistoryVm
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime date { get; set; }
        public double price { get; set; }
        public bool status { get; set; }
        public string paymentMethod { get; set; }
        public IList<OrderVm> items { get; set; }
    }
}
