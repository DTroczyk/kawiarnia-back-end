using System;
using System.Collections.Generic;
using System.Text;

namespace Api.BLL.ViewModel
{
    public class HistoryVm
    {
        public DateTime date { get; set; }
        public string coffeName { get; set; }
        public double price { get; set; }
        public string status { get; set; }
        public string paymentMethod { get; set; }
    }
}
