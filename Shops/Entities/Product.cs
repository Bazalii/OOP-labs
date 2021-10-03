using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        public Product(int id, string name)
        {
            if (id < 0) throw new NegativeValueException("Id of product cannot be negative!");
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public int TotalQuantity { get; set; }
    }
}