namespace Shops.Services
{
    public class Order
    {
        public Order(string productName, int productQuantity)
        {
            ProductName = productName;
            ProductQuantity = productQuantity;
        }

        public string ProductName { get; }

        public int ProductQuantity { get; }
    }
}