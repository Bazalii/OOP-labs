namespace Shops.Services
{
    public class Product
    {
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public int TotalQuantity { get; set; }
    }
}