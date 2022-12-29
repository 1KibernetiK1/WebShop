using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.Models
{
    public class ProductModel
    {
        [Display(Name = "Брэнд")]
        public string Brand { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long ProductId { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public ProductModel()
        { }

        public ProductModel(Product entity)
        {
            ProductId = entity.ProductId;

            Name = entity.Name;
            Description = entity.Description;
            Price = entity.Price;   
            ImageUrl = entity.ImageUrl;
            Brand = entity.ProductBrand.Name;
            Category = entity.ProductCategory.Name;
        }
    }
}
