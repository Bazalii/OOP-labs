using Shops.Tools;

namespace Shops.Entities
{
    public class ProductOrder
    {
        public ProductOrder(string name, int quantity)
        {
            Name = name ?? throw new NullValueException("Name that has been passed is null!");
            if (quantity <= 0) throw new NegativeValueException("Quantity cannot be negative!");
            Quantity = quantity;
        }

        public string Name { get; }

        public int Quantity { get; }
    }
}