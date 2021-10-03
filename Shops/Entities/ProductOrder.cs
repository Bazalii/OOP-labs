using System;
using Shops.Tools;

namespace Shops.Entities
{
    public class ProductOrder
    {
        public ProductOrder(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Quantity cannot be negative!");
            Quantity = quantity;
        }

        public Product Product { get; }

        public int Quantity { get; }
    }
}