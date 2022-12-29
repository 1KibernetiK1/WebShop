using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopAsp2022.Domains
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public long OrderId { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreationDate { get; set; }

        public string DeliveryAddress { get; set; }

        public string ContactPhone { get; set; }

        public int Cost { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<OrderRecord> Records { get; set; } = null;
    }
}
