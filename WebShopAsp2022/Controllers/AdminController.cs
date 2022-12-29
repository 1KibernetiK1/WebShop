﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebShopAsp2022.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
