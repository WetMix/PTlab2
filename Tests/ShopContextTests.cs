using Microsoft.EntityFrameworkCore;
using Shop.DAL;
using Shop.Models;
using Xunit;

namespace Shop.Tests
{
    public class ShopContextTests
    {
        private ShopContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopContext(options);
        }

        [Fact]
        public void CanAddProduct()
        {
            using var ctx = GetContext();
            ctx.Products.Add(new Product
            {
                Name = "Лопата",
                Price = 500,
                Quantity = 10,
                InitialQuantity = 10
            });
            ctx.SaveChanges();

            Assert.Equal(1, ctx.Products.Count());
        }

        [Fact]
        public void CanAddMultipleProducts()
        {
            using var ctx = GetContext();
            ctx.Products.AddRange(
                new Product { Name = "Лопата", Price = 500, Quantity = 10, InitialQuantity = 10 },
                new Product { Name = "Грабли", Price = 400, Quantity = 8, InitialQuantity = 8 }
            );
            ctx.SaveChanges();

            Assert.Equal(2, ctx.Products.Count());
        }

        [Fact]
        public void CanUpdateProduct()
        {
            using var ctx = GetContext();
            var p = new Product { Name = "Лопата", Price = 500, Quantity = 10, InitialQuantity = 10 };
            ctx.Products.Add(p);
            ctx.SaveChanges();

            p.Quantity = 5;
            ctx.SaveChanges();

            Assert.Equal(5, ctx.Products.Find(p.ID)!.Quantity);
        }

        [Fact]
        public void CanDeleteProduct()
        {
            using var ctx = GetContext();
            var p = new Product { Name = "Лопата", Price = 500, Quantity = 10, InitialQuantity = 10 };
            ctx.Products.Add(p);
            ctx.SaveChanges();

            ctx.Products.Remove(p);
            ctx.SaveChanges();

            Assert.Equal(0, ctx.Products.Count());
        }
    }
}
