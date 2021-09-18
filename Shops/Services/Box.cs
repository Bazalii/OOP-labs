namespace Shops.Services
{
    public class Box
    {
        public Box(string productName, int productPrice, int quantity)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public string ProductName { get; private set; }

        public int Quantity { get; set; }

        public int ProductPrice { get; private set; }
    }
}