using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.BLL.Entity
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Coffee")]
        public string CoffeeId { get; set; }
        public Coffee Coffee { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public double Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        // Coffe details

        public int MilkCount { get; set; }
        public int EspressoCount { get; set; }
        public bool IsContainChocolate { get; set; }
    }
}
