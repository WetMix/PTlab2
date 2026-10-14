using Shop.Models;

namespace Shop.DAL
{
    public static class ShopInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Products.Any())
                return;

            context.Products.AddRange(
                new Product { Name = "Лопата", Price = 500, Quantity = 10, InitialQuantity = 10 },
                new Product { Name = "Грабли", Price = 400, Quantity = 8, InitialQuantity = 8 },
                new Product { Name = "Лейка", Price = 300, Quantity = 6, InitialQuantity = 6 }
            );
            context.SaveChanges();
        }
    }
}
