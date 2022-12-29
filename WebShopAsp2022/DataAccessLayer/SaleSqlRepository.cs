using System.Collections.Generic;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.DataAccessLayer
{
    public class SaleSqlRepository
        : IRepository<Sale>
    {
        private readonly ApplicationDbContext _context;

        public Sale FindByName(string name) => null;

        public SaleSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Sale model)
        {
            _context.Sales.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.Sales.Find(id);
            _context.Sales.Remove(entry);
            _context.SaveChanges();
        }

        public IEnumerable<Sale> GetList()
        {
            return _context.Sales;
        }

        public Sale Read(long id)
        {
            var entry = _context.Sales.Find(id);
            return entry;
        }

        public void Update(Sale model)
        {
            var entry = _context.Sales.Find(model.SaleId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            _context.SaveChanges();
        }

        public Sale ReadWithRelations(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
