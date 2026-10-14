using Shop.DAL;

namespace Shop.Services
{
    public class ShopService
    {
        private readonly ShopContext _db;

        public ShopService(ShopContext db)
        {
            _db = db;
        }

        public bool BuyProduct(int productId, int count)
        {
            if (count <= 0)
                return false;

            var product = _db.Products.Find(productId);
            if (product == null || product.Quantity < count)
                return false;

            product.Quantity -= count;

            // Если количество стало меньше половины исходного — цена +20%
            if (product.Quantity < product.InitialQuantity / 2.0)
            {
                product.Price = (int)Math.Round(product.Price * 1.2);
            }

            _db.SaveChanges();
            return true;
        }
    }
}
