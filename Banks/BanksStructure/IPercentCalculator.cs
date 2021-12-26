namespace Banks.BanksStructure
{
    public interface IPercentCalculator : IMyObservable
    {
        float CalculateDepositPercent(float amountOfMoney);

        public float CalculateDebitPercent(float amountOfMoney);

        public float CalculateCreditCommission(float amountOfMoney);
    }
}