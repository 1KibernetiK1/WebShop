using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.Domains;
using WebShopAsp2022.Models;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.Controllers
{
    [Authorize(Roles ="Administrator,SalesManager")]
    public class SalesManagerController : Controller
    {
        private readonly IRepository<Order> _repositoryOrders;
        private readonly UserManager<ApplicationUser> _userManager;

        public SalesManagerController(
            IRepository<Order> repositoryOrders,
            UserManager<ApplicationUser> userManager
            )
        {
            _repositoryOrders = repositoryOrders;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserDetails(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            return View( new UserModel(user) );
        }


        public IActionResult OrdersListView()
        {
            var orders = _repositoryOrders.GetList();
            return View(orders);
        }
    }
}
