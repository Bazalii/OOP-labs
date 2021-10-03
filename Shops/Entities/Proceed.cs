using System;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Proceed
    {
        public Proceed(Shop shop, int proceedValue)
        {
            Shop = shop ?? throw new ArgumentNullException(nameof(shop));
            if (ProceedValue < 0) throw new ArgumentException("Value of proceed cannot be negative!");
            ProceedValue = proceedValue;
        }

        public Shop Shop { get; }

        public int ProceedValue { get; }
    }
}