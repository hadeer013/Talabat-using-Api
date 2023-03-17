using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Data;
using Talabat.DAL.Entities.Identity;
using Talabat.DAL.IdentityContext;

namespace Talabat.Pl
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();
            var services= scope.ServiceProvider;
            var LoggerFactory=services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedData(context, LoggerFactory);

                var UserManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var IdentittyContext = services.GetRequiredService<ApplicationDbContext>();
                await IdentittyContext.Database.MigrateAsync();
                await IdentityContextSeed.SeedAsync(UserManager);
            }
            catch(Exception ex)
            {
                var logger=LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }
            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
