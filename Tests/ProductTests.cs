using Shop.Models;
using Xunit;

namespace Shop.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_DefaultValues_AreSet()
        {
            var p = new Product();

            Assert.Equal(0, p.ID);
            Assert.Equal(string.Empty, p.Name);
            Assert.Equal(0, p.Price);
            Assert.Equal(0, p.Quantity);
            Assert.Equal(0, p.InitialQuantity);
        }

        [Fact]
        public void Product_CanBeInitialized()
        {
            var p = new Product
            {
                ID = 5,
                Name = "Лопата",
                Price = 500,
                Quantity = 10,
                InitialQuantity = 10
            };

            Assert.Equal(5, p.ID);
            Assert.Equal("Лопата", p.Name);
            Assert.Equal(500, p.Price);
            Assert.Equal(10, p.Quantity);
            Assert.Equal(10, p.InitialQuantity);
        }
    }
}
