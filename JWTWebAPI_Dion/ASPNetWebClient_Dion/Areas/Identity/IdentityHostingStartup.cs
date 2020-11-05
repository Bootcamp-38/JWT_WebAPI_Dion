using System;
using ASPNetWebClient_Dion.Areas.Identity.Data;
using ASPNetWebClient_Dion.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ASPNetWebClient_Dion.Areas.Identity.IdentityHostingStartup))]
namespace ASPNetWebClient_Dion.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MyContextAuth>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MyContextConnection")));

                services.AddDefaultIdentity<ASPNetWebClient_DionUser>()
                    .AddEntityFrameworkStores<MyContextAuth>();
            });
        }
    }
}