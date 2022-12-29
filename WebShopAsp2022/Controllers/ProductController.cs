using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.BusinessLogic;
using WebShopAsp2022.Domains;
using WebShopAsp2022.Helpers;
using WebShopAsp2022.Models;

namespace WebShopAsp2022.Controllers
{
    public class ProductController : Controller
    {
        private int _productQuantityPerPage = 20;

        private readonly IRepository<Product> _repositoryProduct;
        private readonly IRepository<Category> _repositoryCategories;
        private readonly IRepository<Brand> _repositoryBrand;

        public ProductController(
            IRepository<Product> repositoryProduct,
            IRepository<Category> repositoryCategories,
            IRepository<Brand> repositoryBrand)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryCategories = repositoryCategories;
            _repositoryBrand = repositoryBrand;
        }
        public IActionResult ProductsFilterReset()
        {
            var filter = new ProductsFilterModel();
            //Сохраняем фильтр в сессию
            HttpContext.SaveToSession<ProductsFilterModel>(filter);
            return RedirectToAction("ListView");
        }
        [HttpPost]
        public IActionResult ProductsFilterApply(ProductsFilterModel filter)
        {
            filter.NeedApply = true;
            //Сохраняем фильтр в сессию
            HttpContext.SaveToSession<ProductsFilterModel>(filter);
            return RedirectToAction("ListView");
        }
        public IActionResult CartView(string urlReturn)
        {
            ViewBag.UrlReturn = urlReturn;
            var cart = HttpContext.LoadFromSession<Cart>();
            return View(cart);
        }

        public IActionResult DecreaseProduct(long id, string urlReturn)
        {
            var product = _repositoryProduct.Read(id);
            if (product == null)
                return View("Error");

            var model = new ProductModel(product);

            // сохранить продукт в корзину
            var cart = HttpContext.LoadFromSession<Cart>();
            cart.RemoveProduct(model);
            HttpContext.SaveToSession(cart);

            return new RedirectResult(urlReturn);
            // return View("CartView", cart);
        }

        public IActionResult IncreaseProduct(long id, string urlReturn)
        {
            var product = _repositoryProduct.Read(id);
            if (product == null)
                return View("Error");

            var model = new ProductModel(product);

            // сохранить продукт в корзину
            var cart = HttpContext.LoadFromSession<Cart>();
            cart.AddProduct(model);
            HttpContext.SaveToSession(cart);

            return new RedirectResult(urlReturn);
            // return View("CartView", cart);
        }

        public IActionResult AddToCart(long id, string urlReturn)
        {
            var product = _repositoryProduct.Read(id);
            if (product == null) 
                return View("Error");

            var model = new ProductModel(product);

            // сохранить продукт в корзину
            var cart = HttpContext.LoadFromSession<Cart>();
            cart.AddProduct(model);
            HttpContext.SaveToSession(cart);

            return new RedirectResult(urlReturn);
            // return View("CartView", cart);
        }

        public IActionResult ListView(string categoryName, int page = 1)
        {
            //Извлекаем фильтр из сессии
            var filter = HttpContext
                .LoadFromSession<ProductsFilterModel>();
            if (filter.ListBrands.Count == 0)
            {
                filter.InitializaBrends(_repositoryBrand.GetList());
            }

            // 0) ищем в базе категорию по имени
            var category = _repositoryCategories
                .FindByName(categoryName);

            // 1) добавляем механизм пагинации (разбиение на страницы)

            var query = _repositoryProduct
                .GetList()
                .Where(p =>
                    category == null ||
                    p.ProductCategory.CategoryId == category.CategoryId);

            //Применим фльтры - если нужно
            if (filter.NeedApply)
            {
                if (filter.MaxPrice > 0 && filter.MaxPrice > filter.MinPrice)
                {
                    query = query.Where(p =>
                        p.Price >= filter.MinPrice &&
                        p.Price <= filter.MaxPrice);
                }
                //фильтрация по брендам
                if (filter.SelectedBrandId > 0)
                {
                    query = query.Where(p =>
                        p.ProductBrand.BrandId == filter.SelectedBrandId);
                }
                //фильтр по названию
                if (!string.IsNullOrEmpty(filter.ProductName))
                {
                    query = query.Where(p =>
                        p.Name.Contains(filter.ProductName, 
                        StringComparison.InvariantCultureIgnoreCase));
                }
            }

            var productsSample = query
                .OrderBy(p => p.ProductId)
                .Skip( (page-1)* _productQuantityPerPage)
                .Take(_productQuantityPerPage)
                .Select(e => new ProductBriefModel(e));

            int totalProductsQuantity = query.Count();

            int pagesQuantity = (int)
                Math.Ceiling(
                totalProductsQuantity / 
                (double) _productQuantityPerPage
                );

            var model = new ProductsPageModel
            {
                TotalProductsQuantity = totalProductsQuantity,
                ProductsFilters = filter,
                CategoryName = categoryName,
                ProductsForPage = productsSample,
                ActivePage = page,
                PagesQuantity = pagesQuantity
            };

            return View(model);
        }

        public IActionResult ProductDetailsView(long id)
        {
            var entity = _repositoryProduct.Read(id);
            var model = new ProductModel(entity);

            return View(model);
        }
    }
}
