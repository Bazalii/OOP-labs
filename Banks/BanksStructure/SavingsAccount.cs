namespace Banks.BanksStructure
{
    public abstract class SavingsAccount : Account
    {
        protected SavingsAccount(int id, int term, int percent, float amountOfMoney, bool doubtfulness, float limitIfIsDoubtful)
        {
            Id = id;
            Term = term;
            Percent = percent;
            AmountOfMoney = amountOfMoney;
            IsDoubtful = doubtfulness;
            LimitIfIsDoubtful = limitIfIsDoubtful;
            DailyPercent = Percent * AmountOfMoney / 365;
        }

        protected float MonthlyIncome { get; set; }

        protected float Percent { get; set; }

        protected float DailyPercent { get; set; }

        public override void AddDailyIncome()
        {
            MonthlyIncome += DailyPercent;
        }

        public void AddMonthlyIncome()
        {
            AmountOfMoney += MonthlyIncome;
        }
    }
}