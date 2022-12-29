using System.Collections.Generic;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.DataAccessLayer
{
    public class OrderRecordSqlRepository
        : IRepository<OrderRecord>
    {
        private readonly ApplicationDbContext _context;

        public OrderRecord FindByName(string name) => null;

        public OrderRecordSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(OrderRecord model)
        {
            _context.OrderRecords.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.OrderRecords.Find(id);
            _context.OrderRecords.Remove(entry);
            _context.SaveChanges();
        }

        public IEnumerable<OrderRecord> GetList()
        {
            return _context.OrderRecords;
        }

        public OrderRecord Read(long id)
        {
            var entry = _context.OrderRecords.Find(id);
            return entry;
        }

        public void Update(OrderRecord model)
        {
            var entry = _context.OrderRecords.Find(model.OrderRecordId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            _context.SaveChanges();
        }

        public OrderRecord ReadWithRelations(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
