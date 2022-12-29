using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;
using WebShopAsp2022.Models;

namespace WebShopAsp2022.Controllers
{
    [Authorize(Roles="Administrator,ContentManager")]
    public class ContentManagerController : Controller
    {
        private readonly IRepository<Product> _repositoryProduct;
        private readonly IRepository<Category> _repositoryCategories;
        private readonly IRepository<Brand> _repositoryBrands;

        public ContentManagerController(
            IRepository<Product> repositoryProduct,
            IRepository<Category> repositoryCategories,
            IRepository<Brand> repositoryBrands)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryCategories = repositoryCategories;
            _repositoryBrands = repositoryBrands;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditCategories()
        {
            var categories = _repositoryCategories.GetList();
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            var category = new Category();
            return View("EditCategory", category);
        }

        public IActionResult DeleteCategory(long id)
        {
            var category = _repositoryCategories.ReadWithRelations(id);
            if (category.ProductsOfCategory.Count() > 0)
            {
                ViewBag.ErrorMessage = "Нельзя удалять категорию с товарами!";
                return View("Error");
            }
            else
            {
                _repositoryCategories.Delete(id);
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditCategory(long id)
        {
            var category = _repositoryCategories.Read(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryId == 0)
                {
                    _repositoryCategories.Create(model);
                }
                else
                {
                    _repositoryCategories.Update(model);
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult AddProduct()
        {
            var categories = _repositoryCategories.GetList();
            var brands = _repositoryBrands.GetList();

            var model = new ProductEditModel(
                categories.ToList(),
                brands.ToList());

            model.UrlReturn = "Index";

            return View("EditProductView", model);
        }

        public IActionResult EditProductView(long id, string urlReturn)
        {
            var entity = _repositoryProduct.Read(id);
            var categories = _repositoryCategories.GetList();
            var brands = _repositoryBrands.GetList();

            var model = new ProductEditModel(
                entity, 
                categories.ToList(),
                brands.ToList());

            model.UrlReturn = urlReturn;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProductView(ProductEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                // Download, Upload
                UploadImage(editModel);

                var model = editModel
                    .AsProduct(_repositoryBrands, _repositoryCategories);

                if (editModel.ProductId == 0)
                {
                    _repositoryProduct.Create(model);
                }
                else
                {
                    _repositoryProduct.Update(model);
                }

                string urlReturn = editModel.UrlReturn;

                return new RedirectResult(urlReturn);
            } 

            return View(editModel);
        }

        private void UploadImage(ProductEditModel editModel)
        {
            if (editModel.ImageFile == null)
            {
                Debug.WriteLine("картинка не найдена");
                return;
            }

            string ext = Path.GetExtension(editModel.ImageFile.FileName);
            string uniqueName = Guid.NewGuid().ToString() + ext;
            string filename = Path.Combine( 
                Directory.GetCurrentDirectory(),
                @"wwwroot\ProductImages",
                uniqueName);

            // сохраняем физический файл на сервер
            using (var stream = System.IO.File.Create(filename))
            {
                editModel.ImageFile.CopyTo(stream);
            }

            // в БД заменить на новое имя файла
            editModel.ImageUrl = uniqueName;
        }
    }
}
