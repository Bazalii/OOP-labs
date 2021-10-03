using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Proceed
    {
        public Proceed(Shop shop, int proceedValue)
        {
            Shop = shop ?? throw new NullValueException("Shop that has been passed is null!");
            if (ProceedValue < 0) throw new NegativeValueException("Value of proceed cannot be negative!");
            ProceedValue = proceedValue;
        }

        public Shop Shop { get; }

        public int ProceedValue { get; }
    }
}