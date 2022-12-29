using System.Collections.Generic;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.DataAccessLayer
{
    public class BrandSqlRepository
        : IRepository<Brand>
    {
        private readonly ApplicationDbContext _context;

        public Brand FindByName(string name) =>
            _context.Brands
                .FirstOrDefault(c => c.Name == name);

        public BrandSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Brand model)
        {
            _context.Brands.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.Brands.Find(id);
            _context.Brands.Remove(entry);
            _context.SaveChanges();
        }

        public IEnumerable<Brand> GetList()
        {
            return _context.Brands;
        }

        public Brand Read(long id)
        {
            var entry = _context.Brands.Find(id);
            return entry;
        }

        public void Update(Brand model)
        {
            var entry = _context.Brands.Find(model.BrandId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            _context.SaveChanges();
        }

        public Brand ReadWithRelations(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
