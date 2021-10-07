using System;

namespace Shops.Entities
{
    public class Proceed
    {
        public Proceed(Shop shop, int proceedValue)
        {
            Shop = shop ?? throw new ArgumentNullException(nameof(shop), "Shop cannot be null!");
            if (ProceedValue < 0) throw new ArgumentException("Value of proceed cannot be negative!", nameof(ProceedValue));
            ProceedValue = proceedValue;
        }

        public Shop Shop { get; }

        public int ProceedValue { get; }
    }
}