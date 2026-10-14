using Microsoft.EntityFrameworkCore;
using Shop.DAL;
using Shop.Models;
using Shop.Services;
using Xunit;

namespace Shop.Tests
{
    public class ShopServiceTests
    {
        private ShopContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var ctx = new ShopContext(options);
            ctx.Products.Add(new Product
            {
                ID = 1,
                Name = "Лопата",
                Price = 500,
                Quantity = 10,
                InitialQuantity = 10
            });
            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public void Buy_DecreasesQuantity()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            service.BuyProduct(1, 1);

            Assert.Equal(9, ctx.Products.Find(1)!.Quantity);
        }

        [Fact]
        public void Buy_MultipleItems_DecreasesQuantity()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            service.BuyProduct(1, 3);

            Assert.Equal(7, ctx.Products.Find(1)!.Quantity);
        }

        [Fact]
        public void Buy_IncreasesPrice_WhenQuantityBelowHalf()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            service.BuyProduct(1, 6); // 10 -> 4, 4 < 5, цена +20%

            var p = ctx.Products.Find(1)!;
            Assert.Equal(4, p.Quantity);
            Assert.Equal(600, p.Price);
        }

        [Fact]
        public void Buy_DoesNotIncreasePrice_WhenQuantityAboveHalf()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            service.BuyProduct(1, 1); // 10 -> 9, 9 >= 5

            var p = ctx.Products.Find(1)!;
            Assert.Equal(9, p.Quantity);
            Assert.Equal(500, p.Price);
        }

        [Fact]
        public void Buy_IncreasesPrice_OnlyOnce_PerPurchase()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            service.BuyProduct(1, 6); // 10 -> 4, цена 600
            service.BuyProduct(1, 1); // 4 -> 3, цена 720

            var p = ctx.Products.Find(1)!;
            Assert.Equal(3, p.Quantity);
            Assert.Equal(720, p.Price);
        }

        [Fact]
        public void Buy_ReturnsFalse_WhenNotEnoughStock()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            bool result = service.BuyProduct(1, 11);

            Assert.False(result);
            Assert.Equal(10, ctx.Products.Find(1)!.Quantity);
        }

        [Fact]
        public void Buy_ReturnsFalse_WhenCountIsZero()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            Assert.False(service.BuyProduct(1, 0));
        }

        [Fact]
        public void Buy_ReturnsFalse_WhenCountIsNegative()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            Assert.False(service.BuyProduct(1, -5));
        }

        [Fact]
        public void Buy_ReturnsFalse_WhenProductNotFound()
        {
            var ctx = GetContext();
            var service = new ShopService(ctx);

            Assert.False(service.BuyProduct(999, 1));
        }
    }
}
