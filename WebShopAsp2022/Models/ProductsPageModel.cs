using System.Collections.Generic;

namespace WebShopAsp2022.Models
{
    /// <summary>
    /// Информация о странице с продуктами
    /// </summary>
    public class ProductsPageModel
    {
        public string CategoryName { get; set; }

        public IEnumerable<ProductBriefModel> ProductsForPage { get; set; }
        
        public int PagesQuantity { get; set; }

        public int ActivePage { get; set; }
        public ProductsFilterModel ProductsFilters { get; internal set; }
        public int TotalProductsQuantity { get; internal set; }
    }
}
