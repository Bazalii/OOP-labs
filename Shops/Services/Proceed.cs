namespace Shops.Services
{
    public class Proceed
    {
        public Proceed(Shop shop, int proceedValue)
        {
            Shop = shop;
            ProceedValue = proceedValue;
        }

        public Shop Shop { get; }

        public int ProceedValue { get; }
    }
}