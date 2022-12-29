using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopAsp2022.Domains
{
    [Table("Categories")]
    public class Category
    { 
        [Key]
        public long CategoryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> ProductsOfCategory { get; set; }
    }
}
