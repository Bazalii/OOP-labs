namespace Banks.BanksStructure
{
    public interface IPercentCalculator
    {
        float CalculateDepositPercent(float amountOfMoney);

        public float CalculateDebitPercent(float amountOfMoney);

        public float CalculateCreditCommission(float amountOfMoney);
    }
}