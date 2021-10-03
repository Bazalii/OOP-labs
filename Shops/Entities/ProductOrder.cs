namespace Shops.Entities
{
    public class ProductOrder
    {
        public ProductOrder(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; }

        public int Quantity { get; }
    }
}