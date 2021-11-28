namespace Banks.BanksStructure.Implementations
{
    public class CreditAccount : Account
    {
        private float _commission;

        public CreditAccount(int id, int term, float commission, float amountOfMoney, bool doubtfulness, float limitIfIsDoubtful)
        {
            Id = id;
            Term = term;
            _commission = commission;
            AmountOfMoney = amountOfMoney;
            IsDoubtful = doubtfulness;
            LimitIfIsDoubtful = limitIfIsDoubtful;
        }

        public override void AddDailyIncome()
        {
            if (AmountOfMoney < 0)
            {
                AmountOfMoney -= _commission;
            }
        }

        public override void WithdrawMoney(float amountOfMoney)
        {
            AmountOfMoney -= amountOfMoney;
        }

        public override void ReduceDaysLeft()
        {
            DaysLeft -= 1;
            if (DaysLeft == 0)
            {
                DaysLeft += 180;
            }
        }
    }
}