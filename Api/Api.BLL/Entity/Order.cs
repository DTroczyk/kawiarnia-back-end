using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Api.BLL.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public User Client { get; set; } 
        public IList<OrderItem> Items { get; set; }
        [NotMapped]
        public double Price => Items == null || Items.Count == 0 ? 0.0 : Items.Sum(i => i.Coffe.Price);
    }
}
