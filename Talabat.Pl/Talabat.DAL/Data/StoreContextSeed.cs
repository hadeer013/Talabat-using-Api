using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.DAL.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedData(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Set<ProductBrand>().Any())
                {
                    var BrandsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    foreach (var brand in Brands)
                        context.Set<ProductBrand>().Add(brand);
                }
                if (!context.Set<ProductType>().Any())
                {
                    var TypesData = File.ReadAllText("../Talabat.DAL/Data/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    foreach (var type in Types)
                        context.Set<ProductType>().Add(type);


                }
                if (!context.Set<Product>().Any())
                {
                    var ProductsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    foreach (var product in products)
                        context.Set<Product>().Add(product);
                }
                if (!context.Set<DeliveryMethod>().Any())
                {
                    var DelivaryMethodData = File.ReadAllText("../Talabat.DAL/Data/SeedData/delivery.json");

                    var DelivaryMethod=JsonSerializer.Deserialize<List<DeliveryMethod>>(DelivaryMethodData);
                    foreach (var method in DelivaryMethod)
                        context.Set<DeliveryMethod>().Add(method);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}