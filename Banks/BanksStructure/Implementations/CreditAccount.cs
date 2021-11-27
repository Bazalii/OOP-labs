namespace Banks.BanksStructure.Implementations
{
    public class CreditAccount : Account
    {
        private int Commission { get; set; }

        public override void AddDailyIncome()
        {
            if (AmountOfMoney < 0)
            {
                AmountOfMoney -= Commission;
            }
        }

        public override void WithdrawMoney(float amountOfMoney)
        {
            AmountOfMoney -= amountOfMoney;
        }
    }
}