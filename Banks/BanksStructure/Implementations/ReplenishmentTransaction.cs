namespace Banks.BanksStructure.Implementations
{
    public class ReplenishmentTransaction : Transaction
    {
        public ReplenishmentTransaction(int id, Account accountToReplenish, float amountOfMoney)
        {
            Id = id;
            AccountToReplenish = accountToReplenish;
            AmountOfMoney = amountOfMoney;
        }

        public Account AccountToReplenish { get; }
    }
}