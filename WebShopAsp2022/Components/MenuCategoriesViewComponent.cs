using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.Components
{
    [ViewComponent]
    public class MenuCategoriesViewComponent:
        ViewComponent

    {
        private readonly IRepository<Category> repository;

        public MenuCategoriesViewComponent(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categoriesNameList = repository
                .GetList()
                .Select(x => x.Name);

            return View(categoriesNameList);
        }
    }
}
