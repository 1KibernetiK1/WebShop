using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.Models
{
    public class ProductsFilterModel
    {
        public bool NeedApply { get; set; } = false;

        [Display(Name = "Поиск по названию товара")]
        public string ProductName { get; set; }

        [Display(Name = "Минимальная цена")]
        public int MinPrice { get; set; }

        [Display(Name = "Максимальная цена")]
        public int MaxPrice { get; set; }

        // фильтр по брендам
        public long SelectedBrandId { get; set; }
        public string Brand { get; set; }

        public List<SelectListItem> ListBrands { get; set; }

        public ProductsFilterModel()
        {
            ListBrands = new List<SelectListItem>();
        }

        public void InitializaBrends(IEnumerable<Brand> brands)
        {
            ListBrands = brands
                .Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.BrandId.ToString()
                })
                .ToList();
            if (SelectedBrandId > 0)
            {
                Brand = brands
                    .FirstOrDefault(b => b
                        .BrandId == SelectedBrandId)
                    ?.Name;
            }
        }
    }
}
