using System;

namespace Banks.BanksStructure.Implementations
{
    public class DepositSumWithPercent
    {
        public DepositSumWithPercent(float sum, float percent)
        {
            if (sum <= 0)
            {
                throw new ArgumentException("Sum of an account should be a positive float!", nameof(sum));
            }

            Sum = sum;
            if (percent <= 0)
            {
                throw new ArgumentException("Percent of an account should be a positive float!", nameof(percent));
            }

            Percent = percent;
        }

        public float Sum { get; }

        public float Percent { get; }
    }
}