using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCGShop.Areas.Identity.Data;
using TCGShop.Data;

[assembly: HostingStartup(typeof(TCGShop.Areas.Identity.IdentityHostingStartup))]
namespace TCGShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TCGShopContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TCGShopContextConnection")));

                services.AddDefaultIdentity<TCGShopUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TCGShopContext>();
            });
        }
    }
}