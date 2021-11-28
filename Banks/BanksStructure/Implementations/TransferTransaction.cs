namespace Banks.BanksStructure.Implementations
{
    public class TransferTransaction : Transaction
    {
        public TransferTransaction(int id, Account accountToWithdraw, Account accountToReplenish, float amountOfMoney)
        {
            Id = id;
            AccountToWithdraw = accountToWithdraw;
            AccountToReplenish = accountToReplenish;
            AmountOfMoney = amountOfMoney;
        }

        public Account AccountToWithdraw { get; }

        public Account AccountToReplenish { get; }
    }
}