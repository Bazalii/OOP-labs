using Shops.Tools;

namespace Shops.Entities
{
    public class ProductOrder
    {
        public ProductOrder(Product product, int quantity)
        {
            Product = product ?? throw new NullValueException("Name that has been passed is null!");
            if (quantity <= 0) throw new NegativeValueException("Quantity cannot be negative!");
            Quantity = quantity;
        }

        public Product Product { get; }

        public int Quantity { get; }
    }
}