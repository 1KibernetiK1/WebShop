using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopAsp2022.Domains;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.DataAccessLayer
{
    // EF Core => Code First
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<OrderRecord> OrderRecords { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public ApplicationDbContext(DbContextOptions opt)
            : base(opt)
        {
        }
    }
}
