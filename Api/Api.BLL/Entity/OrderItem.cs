using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Api.BLL.Entity
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Coffe")]
        public int CoffeId { get; set; }
        public Coffe Coffe { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public double Price { get; set; }

        // Coffe details

        public int MilkCount { get; set; }
        public int EspressoCount { get; set; }
        public bool IsContainChocolate { get; set; }
    }
}
