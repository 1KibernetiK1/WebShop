using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Data;
using WebShopAsp2022.DataAccessLayer;
using WebShopAsp2022.Domains;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = false;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDefaultIdentity<ApplicationUser>(o => {
                    o.Password.RequiredLength = 3;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredUniqueChars = 0;
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<IRepository<Product>, ProductSqlRepository>();
            services.AddTransient<IRepository<Brand>, BrandSqlRepository>();
            services.AddTransient<IRepository<Category>, CategorySqlRepository>();

            services.AddTransient<IRepository<OrderRecord>, OrderRecordSqlRepository>();
            services.AddTransient<IRepository<Order>, OrderSqlRepository>();
            services.AddTransient<IRepository<Sale>, SaleSqlRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /*
                endpoints.MapControllerRoute(
                    name: "navCategories",
                    pattern: "{categoryName}/Page{page}",
                    defaults: new { Controller = "Product", action = "ListView" });

                endpoints.MapControllerRoute(
                    name: "navCategories",
                    pattern: "{categoryName}",
                    defaults: new { Controller = "Product", action = "ListView", page = 1 });

                endpoints.MapControllerRoute(
                    name: "navigationPage",
                    pattern: "Products/Page{page}", // Products/Page10
                    defaults: new { Controller = "Product", action = "ListView" });
                */

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=ListView}/{id?}");

                endpoints.MapRazorPages();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                DataSeeder.SeedProducts(scope.ServiceProvider);
                DataSeeder.SeedRoles(roleManager);
                DataSeeder.SeedUsers(userManager);
            }
        }
    }
}
