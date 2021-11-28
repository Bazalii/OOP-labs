namespace Banks.BanksStructure.Implementations
{
    public class DepositSumWithPercent
    {
        public DepositSumWithPercent(float sum, float percent)
        {
            Sum = sum;
            Percent = percent;
        }

        public float Sum { get; }

        public float Percent { get; }
    }
}