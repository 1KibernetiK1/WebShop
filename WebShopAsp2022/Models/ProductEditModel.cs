using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.Models
{
    public class ProductEditModel
    {
        // для загрузки картинок с клиента на сервер!
        public IFormFile ImageFile { get; set; }

        public Product AsProduct(
            IRepository<Brand> brandRepository,
            IRepository<Category> categoryRepository)
        {
            return new Product
            {
                Description = this.Description,
                ImageUrl = this.ImageUrl,
                Name = this.Name,
                Price = this.Price,
                ProductId = this.ProductId,
                ProductBrand =  brandRepository.Read(SelectedBrandId),
                ProductCategory = categoryRepository.Read(SelectedCategoryId)
            };
        }

        public string UrlReturn { get; set; }

        public long SelectedCategoryId { get; set; }

        public long SelectedBrandId { get; set; }

        public List<SelectListItem> ListCategories { get; set; }

        public List<SelectListItem> ListBrands { get; set; }

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

        public ProductEditModel()
        { }

        public ProductEditModel(
            Product entity, 
            List<Category> categories,
            List<Brand> brands)
        {
            ProductId = entity.ProductId;
            SelectedBrandId = entity.ProductBrand.BrandId;
            SelectedCategoryId = entity.ProductCategory.CategoryId;

            Name = entity.Name;
            Description = entity.Description;
            Price = entity.Price;   
            ImageUrl = entity.ImageUrl;
            Brand = entity.ProductBrand.Name;
            Category = entity.ProductCategory.Name;

            CreateSelectListItems(categories, brands);
        }

        private void CreateSelectListItems(
            List<Category> categories,
            List<Brand> brands)
        {
            ListCategories = categories
                .Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                })
                .ToList();
            ListBrands = brands
                .Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.BrandId.ToString()
                })
                .ToList();
        }

        public ProductEditModel(
            List<Category> categories,
            List<Brand> brands)
        {
            CreateSelectListItems(categories, brands);
        }
    }
}

