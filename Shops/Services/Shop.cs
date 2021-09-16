namespace Shops.Services
{
    public class Shop
    {
        public Shop(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Address { get; private set; }

        public void DeliveryOfProducts(Product product)
        {
        }
    }
}