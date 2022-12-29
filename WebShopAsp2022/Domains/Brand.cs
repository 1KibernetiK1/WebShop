using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopAsp2022.Domains
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        public long BrandId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> ProductsOfBrand { get; set; }
    }
}
