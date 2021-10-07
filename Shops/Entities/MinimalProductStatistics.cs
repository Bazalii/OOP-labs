namespace Shops.Entities
{
    public class MinimalProductStatistics
    {
        public MinimalProductStatistics(
            int minimalPrice,
            int cheapestProductQuantity,
            Shop shopWithCheapestProduct,
            Box boxWithCheapestProduct)
        {
            MinimalPrice = minimalPrice;
            CheapestProductQuantity = cheapestProductQuantity;
            ShopWithCheapestProduct = shopWithCheapestProduct;
            BoxWithCheapestProduct = boxWithCheapestProduct;
        }

        public int MinimalPrice { get; }

        public int CheapestProductQuantity { get; }

        public Shop ShopWithCheapestProduct { get; }

        public Box BoxWithCheapestProduct { get; }
    }
}