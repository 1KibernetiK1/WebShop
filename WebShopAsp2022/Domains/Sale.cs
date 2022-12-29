using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopAsp2022.Domains
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public long SaleId { get; set; }

        public DateTime CreationDate { get; set; }

        public int Cost { get; set; }

        public string SellerId { get; set; } = null;

        public virtual Order SaleOrder { get; set; }
    }
}
