namespace Shop.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }         // текущее количество
        public int InitialQuantity { get; set; }  // изначальное количество
    }
}
