using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.DataAccessLayer
{
    // TODO: заменить все репозитрии одним универсальным
    public class CategorySqlRepository
        : IRepository<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategorySqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Category model)
        {
            var existing = FindByName(model.Name);
            if (existing == null)
            {
                _context.Categories.Add(model);
                _context.SaveChanges();
            }
            else
            {
                model.CategoryId = existing.CategoryId;
                // TODO: бросаем эксепшн?
            }
        }

        public void Delete(long id)
        {
            var entry = _context.Categories.Find(id);
            _context.Categories.Remove(entry);
            _context.SaveChanges();
        }

        public Category FindByName(string name) =>
            _context.Categories
                .FirstOrDefault(c => c.Name == name);
        

        public IEnumerable<Category> GetList()
        {
            return _context.Categories;
        }

        public Category Read(long id)
        {
            var entry = _context.Categories.Find(id);
            return entry;
        }

        public Category ReadWithRelations(long id)
        {
            var entry = _context
                .Categories
                .Include(c => c.ProductsOfCategory)
                .FirstOrDefault(c => c.CategoryId==id);
            return entry;
        }

        public void Update(Category model)
        {
            var entry = _context.Categories.Find(model.CategoryId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            _context.SaveChanges();
        }
    }
}
