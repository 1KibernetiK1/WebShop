using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.BusinessLogic
{
    public class OrderTransactionRequest
    {
        public string ContactPhone { get; set; }

        public string DeliveryAddress { get; set; }

        public ApplicationUser User { get; set; }

        public Cart UserCart { get; set; }

        public IRepository<Order> RepositoryOrder { get; set; }

        public IRepository<OrderRecord> RepositoryOrderRecord { get; set; }

        public IRepository<Product> RepositoryProduct { get; set; }
    }
}
