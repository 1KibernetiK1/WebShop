using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;
using WebShopAsp2022.Models;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.DataAccessLayer
{
    public class DataSeeder
    {
        public static void SeedProducts(IServiceProvider provider)
        {  
            var productRepository = provider
                .GetRequiredService<IRepository<Product>>();
            var brandRepository = provider
                .GetRequiredService<IRepository<Brand>>();
            var categoryRepository = provider
                .GetRequiredService<IRepository<Category>>();
            
            if (productRepository.GetList().Count() > 0)
                return;

            var files = new DirectoryInfo("ProductsJson")
                .GetFiles("*.json");

            foreach (var fi in files)
            {
                string categoryName = Path.GetFileName(fi.FullName);
                categoryName = Path.GetFileNameWithoutExtension(categoryName);

                var category = new Category { Name = categoryName };
                categoryRepository.Create(category);

                string jsonText = File.ReadAllText(fi.FullName);
                var products = JsonConvert
                    .DeserializeObject<List<ProductModel>>(jsonText);

                var branNames = products
                    .Select(p => p.Brand)
                    .Distinct()
                    .ToList();

                branNames.ForEach(b => brandRepository.Create(new Brand { Name = b }));

                foreach (var model in products)
                {
                    var product = new Product
                    {
                        Description = model.Description,
                        Name = model.Name,
                        ImageUrl = model.ImageUrl,
                        Price = model.Price,
                        ProductCategory = category,
                        ProductBrand = brandRepository.FindByName(model.Brand)
                    };

                    productRepository.Create(product);
                }
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.Users.Count() > 0) return;

            var user1 = new ApplicationUser
            {
                UserName = "admin@ya.ru",
                LastName = "Админов",
                FirstName = "Админ",
                Email = "admin@ya.ru",
                EmailConfirmed = true
            };
            IdentityResult userResult = userManager.CreateAsync(user1, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user1, "Administrator").Result;
            }

            var user2 = new ApplicationUser
            {
                UserName = "sales@ya.ru",
                LastName = "Манагер продажи",
                FirstName = "Петя",
                Email = "sales@ya.ru",
                EmailConfirmed = true
            };
            userResult = userManager.CreateAsync(user2, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult =  userManager.AddToRoleAsync(user2, "SalesManager").Result;
            }

            var user3 = new ApplicationUser
            {
                UserName = "content@ya.ru",
                LastName = "Манагер контента",
                FirstName = "Коля",
                Email = "content@ya.ru",
                EmailConfirmed = true
            };
            userResult = userManager.CreateAsync(user3, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user3, "ContentManager").Result;
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = new string[]
            {
                "Administrator",
                "SalesManager",
                "ContentManager",
                "Buyer",
                "Guest"
            };
            if (roleManager.Roles.Count() > 0) return;

            foreach (var roleName in roleNames)
            {
                var role = new IdentityRole { Name = roleName };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
