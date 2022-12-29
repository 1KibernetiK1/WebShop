using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopAsp2022.Domains
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public long ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public virtual Brand ProductBrand { get; set; }

        public virtual Category ProductCategory { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<OrderRecord> OrderRecordsForProduct { get; set; }
    }
}
