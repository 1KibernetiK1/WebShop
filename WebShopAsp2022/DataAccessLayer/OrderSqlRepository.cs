using System.Collections.Generic;
using System.Linq;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.DataAccessLayer
{
    public class OrderSqlRepository
        : IRepository<Order>
    {
        private readonly ApplicationDbContext _context;

        public Order FindByName(string name) =>null;

        public OrderSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Order model)
        {
            _context.Orders.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.Orders.Find(id);
            _context.Orders.Remove(entry);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetList()
        {
            return _context.Orders;
        }

        public Order Read(long id)
        {
            var entry = _context.Orders.Find(id);
            return entry;
        }

        public void Update(Order model)
        {
            var entry = _context.Orders.Find(model.OrderId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            _context.SaveChanges();
        }

        public Order ReadWithRelations(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
