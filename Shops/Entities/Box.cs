using Shops.Tools;

namespace Shops.Entities
{
    public class Box
    {
        public Box()
        {
        }

        public Box(int productId, int productPrice, int quantity)
        {
            ProductId = productId;
            if (productPrice < 0) throw new NegativeValueException("Price of product cannot be negative!");
            ProductPrice = productPrice;
            if (quantity < 0) throw new NegativeValueException("Quantity of product cannot be negative!");
            Quantity = quantity;
        }

        public int ProductId { get; }

        public int Quantity { get; set; }

        public int ProductPrice { get; set; }
    }
}