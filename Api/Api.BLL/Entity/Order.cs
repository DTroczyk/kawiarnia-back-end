using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.BLL.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public User Client { get; set; } 
        public IList<OrderItem> Items { get; set; }

        // Address

        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        // Payment

        public string PaymentMethod { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public string PaymentCard { get; set; }
    }
}
