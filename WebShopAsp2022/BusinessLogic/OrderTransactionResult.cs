using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.BusinessLogic
{
    public class OrderTransactionResult
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public Order Order { get; set; }

        public ApplicationUser User { get; set; }
    }
}
