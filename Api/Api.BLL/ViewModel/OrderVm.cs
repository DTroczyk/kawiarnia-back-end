using System;
using System.Collections.Generic;
using System.Text;

namespace Api.BLL.ViewModel
{
    public class OrderVm
    {
        public string coffeeName { get; set; }
        public int waterCount { get; set; }
        public int espressoCount { get; set; }
        public int milkCount { get; set; }
        public bool isContainChocolate { get; set; }
        public float[] latLng { get; set; }
        public double price { get; set; }
    }
}
