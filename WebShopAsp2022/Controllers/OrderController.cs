using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShopAsp2022.Abstract;
using WebShopAsp2022.BusinessLogic;
using WebShopAsp2022.Domains;
using WebShopAsp2022.Helpers;
using WebShopAsp2022.Models;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.Controllers
{
    [Authorize(Roles = "Guest,Buyer")]
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<OrderRecord> _orderRecordRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(
            IRepository<Order> orderRepository,
            IRepository<Product> productsRepository,
            IRepository<OrderRecord> orderRecordRepository,
            UserManager<ApplicationUser> userManager
            )
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;
            _orderRecordRepository = orderRecordRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterOrder()
        {
            // извлекаем информацию о пользователе
            var user = _userManager
                .GetUserAsync(HttpContext.User)
                .Result;

            // нужна корзина
            var cart = HttpContext.LoadFromSession<Cart>();

            var model = new RegisterOrderViewModel(user, cart);

            return View(model);
        }

        [HttpPost]
        public IActionResult RegisterOrder(RegisterOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // извлекаем информацию о пользователе
                var user = _userManager
                    .GetUserAsync(HttpContext.User)
                    .Result;

                // нужна корзина
                var cart = HttpContext.LoadFromSession<Cart>();

                // регистрируем заказ в системе
                var manager = new ManagerOrderSales();
                var request = new OrderTransactionRequest
                {
                    User = user,
                    UserCart = cart,
                    DeliveryAddress  = model.PostAddress,
                    ContactPhone = model.ContactPhone,
                    RepositoryOrder = _orderRepository,
                    RepositoryOrderRecord = _orderRecordRepository,
                    RepositoryProduct = _productsRepository
                };
                var result = manager.TransactionOrder(request);

                if (result.IsSuccess)
                {
                    // очистка корзины
                    cart = new Cart();
                    // сохраняем в сессию
                    HttpContext.SaveToSession(cart);

                    return View("Success", result);
                }
                else
                {
                    return View("Error");
                }
            }


            return View(model);
        }
    }
}
