using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TCGShop.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EntityContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EntityContext>>()))
            {
                // Look for any data.
                if (context.Products.Any() && context.ProductTypes.Any())
                {
                    return;   // DB has been seeded
                }

                //PRODUCT TYPES
                List<ProductType> productTypes = new List<ProductType>();

                productTypes.Add(new ProductType { Type = "Other" });
                productTypes.Add(new ProductType { Type = "Card" });
                productTypes.Add(new ProductType { Type = "Sealed product" });
                productTypes.Add(new ProductType { Type = "Accesory" });

                context.ProductTypes.AddRange(productTypes);

                //PRODUCTS
                List<Product> products = new List<Product>();

                products.Add(new Product { Title = "Black Lotus", Description = "The Black Lotus", Img = "https://static.cardmarket.com/img/82721fdcc32d450e26c2e52900e58f17/items/1/LEA/5465.jpg", Price = 45000, ProductType = productTypes[1] });
                products.Add(new Product { Title = "Sleeves", Description = "100 Qty", Img = "https://static.cardmarket.com/img/9ed3edab013c43e978786a200f94ff28/items/12/253804.jpg", Price = 7.95f, ProductType = productTypes[3] });
                products.Add(new Product { Title = "Deckbox", Description = "THE BIX BOX", Img = "https://static.cardmarket.com/img/0ec3034e1ebe8ce445dd18abb4288bb5/items/15/292037.jpg", Price = 16.95f, ProductType = productTypes[3] });
                products.Add(new Product { Title = "Booser Box", Description = "36 Boosters", Img = "https://static.cardmarket.com/img/c61639dc051c5a81c719c9bc9ef6111f/items/7/492104.jpg", Price = 129.95f, ProductType = productTypes[2] });

                context.Products.AddRange(products);

                context.SaveChanges();
            }
        }
    }
}