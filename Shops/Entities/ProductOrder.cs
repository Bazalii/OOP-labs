using System;

namespace Shops.Entities
{
    public class ProductOrder
    {
        public ProductOrder(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product), "Product cannot be null!");
            if (quantity <= 0) throw new ArgumentException("Quantity cannot be negative!", nameof(quantity));
            Quantity = quantity;
        }

        public Product Product { get; }

        public int Quantity { get; }
    }
}