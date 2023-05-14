using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Areas.Identity.Data;
using TestProject.Data;

[assembly: HostingStartup(typeof(TestProject.Areas.Identity.IdentityHostingStartup))]
namespace TestProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TestProjectContext1>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TestProjectContext1Connection")));

                services.AddDefaultIdentity<TestProjectUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<TestProjectContext1>().AddDefaultTokenProviders();
            });
        }
    }
}