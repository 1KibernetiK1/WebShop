using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.Models
{
    public class ProductBriefModel
    {
        [HiddenInput(DisplayValue =false)]
        public long ProductId { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        public ProductBriefModel()
        { }

        public ProductBriefModel(Product entity)
        {
            Name = entity.Name;
            Price = entity.Price;
            ProductId = entity.ProductId;
        }
    }
}
