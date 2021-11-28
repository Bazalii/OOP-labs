namespace Banks.BanksStructure.Implementations
{
    public class WithdrawalTransaction : Transaction
    {
        public WithdrawalTransaction(int id, Account accountToWithdraw, float amountOfMoney)
        {
            Id = id;
            AccountToWithdraw = accountToWithdraw;
            AmountOfMoney = amountOfMoney;
        }

        public Account AccountToWithdraw { get; }
    }
}