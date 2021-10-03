using Shops.Tools;

namespace Shops.Entities
{
    public class Box
    {
        public Box()
        {
            Product = null;
            Quantity = 0;
            ProductPrice = 0;
        }

        public Box(Product product, int productPrice, int quantity)
        {
            Product = product;
            if (productPrice < 0) throw new NegativeValueException("Price of product cannot be negative!");
            ProductPrice = productPrice;
            if (quantity < 0) throw new NegativeValueException("Quantity of product cannot be negative!");
            Quantity = quantity;
        }

        public Product Product { get; }

        public int Quantity { get; set; }

        public int ProductPrice { get; set; }
    }
}