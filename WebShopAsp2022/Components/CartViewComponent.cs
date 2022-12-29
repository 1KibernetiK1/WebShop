using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebShopAsp2022.BusinessLogic;
using WebShopAsp2022.Helpers;

namespace WebShopAsp2022.Components
{
    [ViewComponent]
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.LoadFromSession<Cart>();
            return View(cart);
        }
    }
}
