using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopAsp2022.Domains
{
    [Table("OrderRecords")]
    public class OrderRecord
    {
        [Key]
        public long OrderRecordId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public virtual Order OrderForRecord { get; set; }
    }
}
