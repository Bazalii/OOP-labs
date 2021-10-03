using System;

namespace Shops.Entities
{
    public class Product
    {
        public Product(int id, string name)
        {
            if (id < 0) throw new ArgumentException("Id of product cannot be negative!", nameof(id));
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null!");
        }

        public int Id { get; }

        public string Name { get; }

        public int TotalQuantity { get; set; }
    }
}