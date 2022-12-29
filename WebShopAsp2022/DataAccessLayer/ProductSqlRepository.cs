using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.DataAccessLayer
{
    public class ProductSqlRepository
        : IRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product FindByName(string name) =>
            _context.Products
                .FirstOrDefault(c => c.Name == name);

        public void Create(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.Products.Find(id);
            _context.Products.Remove(entry);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetList()
        {
            return _context
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductBrand);
        }

        public Product Read(long id)
        {   // join tables
            var entry = _context
                .Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductCategory)
                .FirstOrDefault(p => p.ProductId == id);
            return entry;
        }

        public void Update(Product model)
        {
            var entry = _context.Products.Find(model.ProductId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            entry.ProductCategory = model.ProductCategory;
            entry.ProductBrand = model.ProductBrand;
            _context.SaveChanges();
        }

        public Product ReadWithRelations(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
