namespace Shops.Entities
{
    public class Box
    {
        public Box()
        {
        }

        public Box(int productId, int productPrice, int quantity)
        {
            ProductId = productId;
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public int ProductId { get; }

        public int Quantity { get; set; }

        public int ProductPrice { get; set; }
    }
}