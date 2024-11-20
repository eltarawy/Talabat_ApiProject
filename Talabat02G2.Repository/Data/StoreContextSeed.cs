using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Entities.OrderAggregtion;

namespace TalabatG02.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {

            if (!dbContext.ProductBrands.Any())
            {
                var brandsData = @"[{
        ""Name"": ""Starbucks""
    },
    {
        ""Name"": ""Costa""
    },
    {
        ""Name"": ""Cilantro""
    },
    {
        ""Name"": ""TBS""
    },
    {
        ""Name"": ""On The Run""
    },
    {
        ""Name"": ""Caribou""
    },
    {
        ""Name"": ""Krispy Kreme""
    }
]";
                                 //File.ReadAllText("../TalabatG02.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        await dbContext.ProductBrands.AddAsync(brand);


                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.ProductTypes.Any())
            {
                var TypeData = @"[{
        
        ""Name"": ""Frappuccino""
    },
    {
       
        ""Name"": ""Latte""
    },
    {
        ""Name"": ""Mocha""
    },
    {
       
        ""Name"": ""Macchiato""
    },
    {
       
        ""Name"": ""Matcha""
    },
    {
        ""Name"": ""Cake""
    },
    {
        
        ""Name"": ""Donuts""
    },
    {
        
        ""Name"": ""Salad""
    }
]";
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                        await dbContext.ProductTypes.AddAsync(type);


                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.products.Any())
            {
                var productsData = @"[{
        ""Name"": ""Double Caramel Frappuccino"",
        ""Description"": ""Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna."",
        ""Price"": 200,
        ""PictureUrl"": ""images/products/sb-ang1.png"",
        ""ProductTypeId"": 2,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""White Chocolate Mocha Frappuccino"",
        ""Description"": ""Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus."",
        ""Price"": 150,
        ""PictureUrl"": ""images/products/sb-ang2.png"",
        ""ProductTypeId"": 2,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""Iced Cafe Latte"",
        ""Description"": ""Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy."",
        ""Price"": 180,
        ""PictureUrl"": ""images/products/sb-core1.png"",
        ""ProductTypeId"": 2,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""White Chocolate Mocha"",
        ""Description"": ""Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci."",
        ""Price"": 300,
        ""PictureUrl"": ""images/products/sb-core2.png"",
        ""ProductTypeId"": 3,
        ""ProductBrandId"": 2
    },
    {
        ""Name"": ""Iced Caramel Macchiato"",
        ""Description"": ""Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna."",
        ""Price"": 250,
        ""PictureUrl"": ""images/products/sb-react1.png"",
        ""ProductTypeId"": 4,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""Hot Caramel Macchiato"",
        ""Description"": ""Aenean nec lorem. In porttitor. Donec laoreet nonummy augue."",
        ""Price"": 120,
        ""PictureUrl"": ""images/products/sb-ts1.png"",
        ""ProductTypeId"": 4,
        ""ProductBrandId"": 5
    },
    {
        ""Name"": ""Iced Matcha Latte"",
        ""Description"": ""Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna."",
        ""Price"": 10,
        ""PictureUrl"": ""images/products/hat-core1.png"",
        ""ProductTypeId"": 5,
        ""ProductBrandId"": 2
    },
    {
        ""Name"": ""Honey Cake"",
        ""Description"": ""Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy."",
        ""Price"": 8,
        ""PictureUrl"": ""images/products/hat-react1.png"",
        ""ProductTypeId"": 6,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""Blueberry Cheesecake"",
        ""Description"": ""Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna."",
        ""Price"": 15,
        ""PictureUrl"": ""images/products/hat-react2.png"",
        ""ProductTypeId"": 6,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""Glazed Donuts"",
        ""Description"": ""Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus."",
        ""Price"": 18,
        ""PictureUrl"": ""images/products/glove-code1.png"",
        ""ProductTypeId"": 7,
        ""ProductBrandId"": 3
    },
    {
        ""Name"": ""Greek Salad"",
        ""Description"": ""Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci."",
        ""Price"": 15,
        ""PictureUrl"": ""images/products/glove-code2.png"",
        ""ProductTypeId"": 7,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""Purple React Gloves"",
        ""Description"": ""Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa."",
        ""Price"": 16,
        ""PictureUrl"": ""images/products/glove-react1.png"",
        ""ProductTypeId"": 4,
        ""ProductBrandId"": 4
    },
    {
        ""Name"": ""Green React Gloves"",
        ""Description"": ""Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci."",
        ""Price"": 14,
        ""PictureUrl"": ""images/products/glove-react2.png"",
        ""ProductTypeId"": 4,
        ""ProductBrandId"": 4
    },
    {
        ""Name"": ""Redis Red Boots"",
        ""Description"": ""Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy."",
        ""Price"": 250,
        ""PictureUrl"": ""images/products/boot-redis1.png"",
        ""ProductTypeId"": 3,
        ""ProductBrandId"": 6
    },
    {
        ""Name"": ""Core Red Boots"",
        ""Description"": ""Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna."",
        ""Price"": 189.99,
        ""PictureUrl"": ""images/products/boot-core2.png"",
        ""ProductTypeId"": 3,
        ""ProductBrandId"": 2
    },
    {
        ""Name"": ""Core Purple Boots"",
        ""Description"": ""Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci."",
        ""Price"": 199.99,
        ""PictureUrl"": ""images/products/boot-core1.png"",
        ""ProductTypeId"": 3,
        ""ProductBrandId"": 2
    },
    {
        ""Name"": ""Angular Purple Boots"",
        ""Description"": ""Aenean nec lorem. In porttitor. Donec laoreet nonummy augue."",
        ""Price"": 150,
        ""PictureUrl"": ""images/products/boot-ang2.png"",
        ""ProductTypeId"": 3,
        ""ProductBrandId"": 1
    },
    {
        ""Name"": ""Angular Blue Boots"",
        ""Description"": ""Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy."",
        ""Price"": 180,
        ""PictureUrl"": ""images/products/boot-ang1.png"",
        ""ProductTypeId"": 3,
        ""ProductBrandId"": 1
    }
]";
                //File.ReadAllText("../TalabatG02.Repository/Data/DataSeed/brands.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await dbContext.products.AddAsync(product);


                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.DeliveryMethods.Any())
            {
                var MethodsData = @"[{    ""ShortName"": ""UPS1"",
    ""Description"": ""Fastest delivery time"",
    ""DeliveryTime"": ""1-2 Days"",
    ""Cost"": 10
  },
  {
    ""ShortName"": ""UPS2"",
    ""Description"": ""Get it within 5 days"",
    ""DeliveryTime"": ""2-5 Days"",
    ""Cost"": 5
  },
  {
    ""ShortName"": ""UPS3"",
    ""Description"": ""Slower but cheap"",
    ""DeliveryTime"": ""5-10 Days"",
    ""Cost"": 2
  },
  {
    ""ShortName"": ""FREE"",
    ""Description"": ""Free! You get what you pay for"",
    ""DeliveryTime"": ""1-2 Weeks"",
    ""Cost"": 0}]";
                //File.ReadAllText("../TalabatG02.Repository/Data/DataSeed/brands.json");
                var DeliveryMethods  = JsonSerializer.Deserialize<List<DeliveryMethod>>(MethodsData);

                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var method in DeliveryMethods)
                        await dbContext.DeliveryMethods.AddAsync(method);


                    await dbContext.SaveChangesAsync();
                }
            }
           
        }
    }
}
