using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebShopAsp2022.DataAccessLayer;
using WebShopAsp2022.UsersRoles;

[assembly: HostingStartup(typeof(WebShopAsp2022.Areas.Identity.IdentityHostingStartup))]
namespace WebShopAsp2022.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}