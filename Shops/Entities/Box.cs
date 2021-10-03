using System;

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
            Product = product ?? throw new ArgumentNullException(nameof(product), "Product cannot be null!");
            if (productPrice < 0) throw new ArgumentException("Price of product cannot be negative!", nameof(productPrice));
            ProductPrice = productPrice;
            if (quantity < 0) throw new ArgumentException("Quantity of product cannot be negative!", nameof(productPrice));
            Quantity = quantity;
        }

        public Product Product { get; }

        public int Quantity { get; set; }

        public int ProductPrice { get; set; }
    }
}